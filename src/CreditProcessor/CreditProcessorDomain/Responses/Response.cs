namespace CreditProcessor.Domain.Responses
{
    public class Response
    {
        public Response(object message)
        {
            Message = message;
            Errors = new List<string>();
        }

        public object Message { get; set; }
        public List<string> Errors { get; set; }
    }
}
