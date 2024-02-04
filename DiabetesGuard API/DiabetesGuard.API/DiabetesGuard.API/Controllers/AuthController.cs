using DiabetesGuard.API.Models.Domain;
using DiabetesGuard.API.Models.Dto;
using DiabetesGuard.API.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace DiabetesGuard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ITokenRepositry tokenRepositry;
        private readonly IConfiguration configuration;

        public AuthController(UserManager<User> userManager,
            ITokenRepositry tokenRepositry, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.tokenRepositry=tokenRepositry;
            this.configuration = configuration;
        }

       

        //POST: {baseApiUrl}/api/auth/login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var identityUser = await userManager.FindByEmailAsync(request.Email) ;

            if(identityUser is not null) {

                var checkPasswordResult = await userManager.CheckPasswordAsync(identityUser,request.Password);
                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(identityUser);
                    //Create a token and response
                    var jwtToken = tokenRepositry.CreateJwtToken(identityUser, roles.ToList());

                    
                 

                    var refreshToken = GenerateRefreshToken();
                    // SetRefreshToken(refreshToken,identityUser);
                    identityUser.RefreshToken = refreshToken.Token;
                    identityUser.TokenCreated = refreshToken.CreatedDate;
                    identityUser.TokenExpires = refreshToken.Expires;

                    await userManager.UpdateAsync(identityUser);

                    var response = new LoginResponseDto()
                    {
                        Id = identityUser.Id,
                        Email = request.Email,
                        Roles = roles.ToList(),
                        Token = jwtToken,
                        RefreshToken=refreshToken.Token

                    };

                    return Ok(response);

                }

            }

            ModelState.AddModelError("", "Email or Password is incorrect");

            return ValidationProblem(ModelState);


        }


        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {

                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)),
                Expires = DateTime.Now.AddDays(7)

            };

            return refreshToken;
        }

        [HttpPost("Refresh")]

        public async Task<IActionResult> Refresh([FromBody] RefreshModel model)
        {
            var principal = GetPrincipalFromExpiredToken(model.AccessToken);

            if (principal?.Identity?.Name is null)
                return Unauthorized();


            var user = await userManager.FindByNameAsync(principal.Identity.Name);

            if(user is null || user.RefreshToken!=model.RefreshToken || user.TokenExpires < DateTime.UtcNow)
               return Unauthorized();

            var roles = await userManager.GetRolesAsync(user);
            var jwtToken = tokenRepositry.CreateJwtToken(user, roles.ToList());

            return Ok(new LoginResponseDto
            {
                Token = jwtToken,
                RefreshToken= model.RefreshToken
            }) ;




        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {

            var secret= configuration["JWT:Secret"]?? throw new InvalidOperationException("Secret Not Configured");
           
            var validation = new TokenValidationParameters
            {
               
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey =
                        new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                ValidateLifetime = false
            };

            return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
        }



        private void SetRefreshToken(RefreshToken newRefreshToken, User user)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(7)
            };

            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.CreatedDate;
            user.TokenExpires = newRefreshToken.Expires;

        }


        //POST : {baseApiUrl}/api/auth/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            //Create IdentityUser class Obj
            var user = new User
            {
                UserName = request.Username,
                Email = request.Email?.Trim(),
                DateOfBirth=request.DateOfBirth,
                Gender=request.Gender                 

            };

            var identityResult = await userManager.CreateAsync(user, request.Password);
            if (identityResult.Succeeded)
            {
                identityResult = await userManager.AddToRoleAsync(user, "Reader");
                if (identityResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    if (identityResult.Errors.Any())
                    {
                        foreach (var error in identityResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }

                }
            }
            else
            {
                if(identityResult.Errors.Any())
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("",error.Description);
                    }
                }
            }

            return ValidationProblem(ModelState);
        }




    }
}
