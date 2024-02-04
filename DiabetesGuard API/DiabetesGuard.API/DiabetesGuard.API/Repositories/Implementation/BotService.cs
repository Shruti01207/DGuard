using DiabetesGuard.API.Models.Domain;
using DiabetesGuard.API.Repositories.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Bot.Connector.DirectLine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiabetesGuard.API.Repositories.Implementation
{
    public class BotService : IBotService
    {
        private HttpClient _httpClient;
        private static string _watermark = null;
        private readonly IConfiguration configuration;

        public BotService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            this.configuration = configuration;
        }

        public async Task<List<Activity>> GetBotResponseActivitiesAsync(string token,string conversationId)
        {
                  ActivitySet response = null;
                  List<Activity> result = new List<Activity>();
                   using (var directLineClient = new DirectLineClient(token))
                   {
                     response = await directLineClient.Conversations.GetActivitiesAsync(conversationId, null);
                      if (response == null)
                     {
                       directLineClient.Dispose();
                      
                     }
                   }

                _watermark = response?.Watermark;
                result = response?.Activities?.Where(x =>
                  x.Type == ActivityTypes.Message &&
                    string.Equals(x.From.Name, configuration.GetValue<string>("Settings:BotName"), StringComparison.Ordinal)).ToList();

                if (result != null && result.Any())
                {
                    return result;
                }

                return new List<Activity>();
        }

        public async Task<Conversation> GetConversationId(string token)
        {
           
            using (var directLineClient = new DirectLineClient(token))
            {
                var conversation = await directLineClient.Conversations.StartConversationAsync();
                return conversation;
             
            }
            
        }

        public async Task<DirectLineToken> GetTokenAsync(string url)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<DirectLineToken>(url);
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
        }

        public async Task<ResourceResponse> PostMessage(string message, string token, string conversationId)
        {
            using (var directLineClient = new DirectLineClient(token))
            {
                
              
                    // Send user message using directlineClient
               var obj=
                    await directLineClient.Conversations.PostActivityAsync(conversationId, new Activity()
                      {
                        Type = ActivityTypes.Message,
                        From = new ChannelAccount { Id = "userId", Name = "userName" },
                        Text = message,
                        TextFormat = "plain",
                        Locale = "en-Us",
                       });

                return obj;
            }
    }

        public async Task<DirectLineClient> RefreshToken(string currentToken)
        {
            // DirectLine provides a token refresh method
            // Requires the currentToken valid when refreshing
            string refreshToken = (await new DirectLineClient(currentToken).Tokens.RefreshTokenAsync()).Token;
            // create a new directline client with refreshToken
            DirectLineClient directLineClient = new DirectLineClient(refreshToken);

            return directLineClient;
            // use new directLineClient to communicate to your bot
           

        }
    }
}
