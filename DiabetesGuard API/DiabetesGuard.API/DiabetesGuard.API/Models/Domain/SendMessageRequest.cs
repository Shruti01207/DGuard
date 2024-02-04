namespace DiabetesGuard.API.Models.Domain
{
    public class SendMessageRequest
    {
        public string Message { get; set; }

        public string Token  { get; set; }

        public string ConversationId { get; set; }


    }
}
