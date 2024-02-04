using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace DiabetesGuard.API.Repositories.Interface
{
    public interface ITokenRepositry
    {
        public string CreateJwtToken(IdentityUser user, List<string> roles);
   


    }
}
