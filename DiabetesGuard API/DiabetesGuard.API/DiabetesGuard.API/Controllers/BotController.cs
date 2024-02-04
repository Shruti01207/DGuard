using DiabetesGuard.API.Models.Domain;
using DiabetesGuard.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DiabetesGuard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly IBotService botService;
        private readonly IConfiguration configuration;

        public BotController(IBotService botService, IConfiguration configuration)
        {
            this.botService = botService;
            this.configuration = configuration;
        }

        [HttpGet]
        [Route("token")]
        public async Task<IActionResult> GetDirectLineToken()
        {
            var token = await botService.GetTokenAsync(configuration.GetValue<string>("Settings:BotTokenEndpoint"));

            if (token != null)
            {
                return Ok(token);
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("conversation")]
        public async Task<IActionResult> StartConversation()
        {
            var directLineToken = await botService.GetTokenAsync(configuration.GetValue<string>("Settings:BotTokenEndpoint"));

            var response = await botService.GetConversationId(directLineToken.Token);

            if (response != null)
                return Ok(response);

            return BadRequest();
        }

        [HttpPost]
        [Route("user_message")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
        {
           // var directLineToken = await botService.GetTokenAsync(configuration.GetValue<string>("Settings:BotTokenEndpoint"));

            var response = await botService.PostMessage(request.Message, request.Token, request.ConversationId);
         
            if (response != null)
            {
                return Ok(response);
            }

            return BadRequest();

        }

        [HttpPost]
        [Route("bot_message")]
        public async Task<IActionResult> GetBotResponse([FromBody] BotResponseRequest request)
        {

            var response = await botService.GetBotResponseActivitiesAsync(request.Token, request.ConversationId);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();

        }

        [HttpPost]
        [Route("refresh_token")]

        public async Task<IActionResult> RefreshDirectLineToken([FromBody] string currentToken)
        {
            var response =  await botService.RefreshToken(currentToken);

            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }



    }
}
