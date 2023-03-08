using CreditProcessor.Domain.ValueObjects;

namespace CreditProcessor.Domain.Entities
{
    public class Credit : BaseEntity
    {
        public Credit(decimal totalAmount, CreditType creditType) : base()
        {
            TotalAmount = totalAmount;
            CreditType = creditType;
            Installments = new List<Installment>();
        }

        public decimal TotalAmount { get; private set; }
        public decimal TotalAmountWithTax { get; private set; }
        public decimal TaxAmount { get; private set; }
        public CreditType CreditType { get; private set; }
        public CreditStatus CreditStatus { get; private set; }
        public virtual List<Installment> Installments { get; private set; }

        public void SetStatus(bool isValid) => CreditStatus = isValid ? CreditStatus.Approved : CreditStatus.Rejected;

        public void CalculateCredit(int installmentsCount, DateTime firstDueDate)
        {
            TaxAmount = TotalAmount * GetTaxPercentage();
            TotalAmountWithTax = TotalAmount + TaxAmount;

            Installments.Clear();

            var installmentAmount = TotalAmount / installmentsCount;
            var installmentAmountWithTax = TotalAmountWithTax / installmentsCount;
            var installmentTaxAmount = TaxAmount / installmentsCount;

            for (var i = 0; i < installmentsCount; i++)
            {
                Installments.Add(new Installment
                {
                    Amount = installmentAmount,
                    AmountWithTax = installmentAmountWithTax,
                    TaxAmount = installmentTaxAmount,
                    DueDate = firstDueDate.AddMonths(i),
                    Number = i
                });
            }
        }

        private decimal GetTaxPercentage()
        {
            return CreditType switch
            {
                CreditType.Direct => 0.02m,
                CreditType.Consigned => 0.01m,
                CreditType.LegalEntity => 0.05m,
                CreditType.Individual => 0.03m,
                CreditType.RealEstate => 0.09m,
                _ => 0.0m,
            };
        }
    }
}
