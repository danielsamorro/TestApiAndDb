using CreditProcessor.Domain.Commands.Requests;
using CreditProcessor.Domain.Commands.Responses;
using CreditProcessor.Domain.Commands.Validators;
using CreditProcessor.Domain.Entities;
using CreditProcessor.Domain.Responses;
using CreditProcessor.Domain.UnitOfWork;
using CreditProcessor.Domain.ValueObjects;
using MediatR;

namespace CreditProcessor.Domain.Commands.Handlers
{
    public class ProcessCreditHandler : IRequestHandler<ProcessCreditRequest, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ProcessCreditRequestValidator _validator;

        public ProcessCreditHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _validator = new ProcessCreditRequestValidator();
        }

        public async Task<Response> Handle(ProcessCreditRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = _validator.Validate(request);

                var credit = new Credit(request.CreditValue, request.CreditType);

                credit.SetStatus(validationResult.IsValid);
                
                if (credit.CreditStatus == CreditStatus.Approved)
                    credit.CalculateCredit(request.InstallmentsCount, request.FirstDueDate);              

                _unitOfWork.CreditRepository.Add(credit);

                await _unitOfWork.SaveChangesAsync();

                return new Response(new ProcessCreditResponse
                {
                    CreditStatus = credit.CreditStatus.ToString(),
                    TotalAmountWithTax = credit.TotalAmountWithTax,
                    TaxAmount = credit.TaxAmount
                });
            }
            catch (Exception ex)
            {
                return new ErrorResponse(ex.Message);
            }
        }
    }
}
