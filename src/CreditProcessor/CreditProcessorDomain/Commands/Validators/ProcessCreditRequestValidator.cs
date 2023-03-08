using CreditProcessor.Domain.Commands.Requests;
using FluentValidation;

namespace CreditProcessor.Domain.Commands.Validators
{
    public class ProcessCreditRequestValidator : AbstractValidator<ProcessCreditRequest>
    {
        public ProcessCreditRequestValidator()
        {
            RuleFor(request => request.CreditValue).LessThanOrEqualTo(1000000.00m);
            RuleFor(request => request.InstallmentsCount).InclusiveBetween(5, 72);
            When(request => request.CreditType == ValueObjects.CreditType.LegalEntity, () =>
            {
                RuleFor(x => x.CreditValue).GreaterThanOrEqualTo(15000.00m);
            });
            RuleFor(request => request.FirstDueDate).InclusiveBetween(DateTime.Now.AddDays(15), DateTime.Now.AddDays(40));
        }
    }
}
