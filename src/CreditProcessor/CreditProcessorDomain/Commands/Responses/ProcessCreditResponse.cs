namespace CreditProcessor.Domain.Commands.Responses
{
    public class ProcessCreditResponse
    {
        public string CreditStatus { get; set; }
        public decimal TotalAmountWithTax { get; set; }
        public decimal TaxAmount { get; set; }
    }
}
