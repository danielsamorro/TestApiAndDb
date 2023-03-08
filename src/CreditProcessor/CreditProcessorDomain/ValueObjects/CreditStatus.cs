using System.ComponentModel.DataAnnotations;

namespace CreditProcessor.Domain.ValueObjects
{
    public enum CreditStatus
    {
        [Display(Name = "Aprovado")]
        Approved,
        [Display(Name = "Reprovado")]
        Rejected
    }
}
