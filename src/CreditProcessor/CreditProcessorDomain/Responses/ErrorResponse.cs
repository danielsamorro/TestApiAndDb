namespace CreditProcessor.Domain.Responses
{
    public class ErrorResponse : Response
    {
        public ErrorResponse(params string[] errors) : base("")
        {
            Errors = errors.ToList();
        }
    }
}
