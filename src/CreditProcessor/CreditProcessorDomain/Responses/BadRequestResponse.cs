namespace CreditProcessor.Domain.Responses
{
    public class BadRequestResponse : Response
    {
        public BadRequestResponse(params string[] errors) : base("")
        {
            Errors = errors.ToList();
        }
    }
}
