using CreditProcessor.Domain.Responses;
using CreditProcessor.Domain.ValueObjects;
using MediatR;

namespace CreditProcessor.Domain.Commands.Requests
{
    public class ProcessCreditRequest : IRequest<Response>
    {
        public decimal CreditValue { get; set; }
        public CreditType CreditType { get; set; }
        public int InstallmentsCount { get; set; }
        public DateTime FirstDueDate { get; set; }
    }
}
