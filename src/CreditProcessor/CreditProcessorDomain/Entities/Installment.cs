namespace CreditProcessor.Domain.Entities
{
    public class Installment : BaseEntity
    {
        public Installment() : base()
        {

        }

        public int Number { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountWithTax { get; set; }
        public decimal TaxAmount { get; set; }
        public DateTime DueDate { get; set; }

        public virtual Credit Credit { get; set; }
        public Guid CreditId { get; set; }
    }
}
