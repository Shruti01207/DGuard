using DiabetesGuard.API.Models.Domain;
using Microsoft.Bot.Connector.DirectLine;

namespace DiabetesGuard.API.Repositories.Interface
{
    public interface IBotService
    {   
        Task<DirectLineToken> GetTokenAsync(string url);

        Task<Conversation> GetConversationId(string token);

        Task<ResourceResponse> PostMessage(string message,string token, string conversationId);

        Task<List<Activity>> GetBotResponseActivitiesAsync(string token, string conversationId);

        Task<DirectLineClient> RefreshToken(string token);
    }
}
