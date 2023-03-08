using CreditProcessor.Domain.Commands.Handlers;
using CreditProcessor.Domain.Commands.Requests;
using CreditProcessor.Domain.Commands.Responses;
using CreditProcessor.Domain.Entities;
using CreditProcessor.Domain.Responses;
using CreditProcessor.Domain.UnitOfWork;
using CreditProcessor.Domain.ValueObjects;
using Moq;

namespace CreditProcessor.UnitTests.Domain.Commands.Handlers
{
    public class ProcessCreditHandlerTests
    {
        private readonly MockRepository _mockRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly ProcessCreditHandler _handler;

        public ProcessCreditHandlerTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _mockUnitOfWork = _mockRepository.Create<IUnitOfWork>();
            _handler = new ProcessCreditHandler(_mockUnitOfWork.Object);
        }

        [Theory]
        [InlineData(CreditType.Direct, 0.02)]
        [InlineData(CreditType.Consigned, 0.01)]
        [InlineData(CreditType.LegalEntity, 0.05)]
        [InlineData(CreditType.Individual, 0.03)]
        [InlineData(CreditType.RealEstate, 0.09)]
        public async Task Handle_Should_Approve_Credit(CreditType creditType, decimal taxPercentage)
        {
            var request = new ProcessCreditRequest
            {
                CreditType = creditType,
                CreditValue = 20000.00m,
                FirstDueDate = DateTime.UtcNow.AddDays(15),
                InstallmentsCount = 10
            };

            _mockUnitOfWork.Setup(m => m.CreditRepository.Add(It.IsAny<Credit>()));
            _mockUnitOfWork.Setup(m => m.SaveChangesAsync()).Returns(Task.FromResult(1));

            var response = await _handler.Handle(request, default);

            _mockRepository.VerifyAll();

            Assert.NotNull(response);
            Assert.IsType<Response>(response);

            var resultMessage = (ProcessCreditResponse)response.Message;

            Assert.NotNull(resultMessage);
            Assert.Equal(CreditStatus.Approved.ToString(), resultMessage.CreditStatus);
            Assert.Equal(request.CreditValue * taxPercentage, resultMessage.TaxAmount);
            Assert.Equal(request.CreditValue + (request.CreditValue * taxPercentage), resultMessage.TotalAmountWithTax);

        }

        [Fact]
        public async Task Handle_Should_Reject_Credit()
        {
            var request = new ProcessCreditRequest
            {
                CreditType = CreditType.LegalEntity,
                CreditValue = 10000.00m,
                FirstDueDate = DateTime.UtcNow.AddDays(15),
                InstallmentsCount = 10
            };

            _mockUnitOfWork.Setup(m => m.CreditRepository.Add(It.IsAny<Credit>()));
            _mockUnitOfWork.Setup(m => m.SaveChangesAsync()).Returns(Task.FromResult(1));

            var response = await _handler.Handle(request, default);

            _mockRepository.VerifyAll();

            Assert.NotNull(response);
            Assert.IsType<Response>(response);

            var resultMessage = (ProcessCreditResponse)response.Message;

            Assert.NotNull(resultMessage);
            Assert.Equal(CreditStatus.Rejected.ToString(), resultMessage.CreditStatus);

        }

        [Fact]
        public async Task Handle_Should_Return_Error()
        {
            var request = new ProcessCreditRequest
            {
                CreditType = CreditType.Direct,
                CreditValue = 10000.00m,
                FirstDueDate = DateTime.UtcNow.AddDays(15),
                InstallmentsCount = 10
            };

            _mockUnitOfWork.Setup(m => m.CreditRepository.Add(It.IsAny<Credit>()));
            _mockUnitOfWork.Setup(m => m.SaveChangesAsync()).Throws(new Exception("Error"));

            var response = await _handler.Handle(request, default);

            _mockRepository.VerifyAll();

            Assert.NotNull(response);
            Assert.IsType<ErrorResponse>(response);
        }
    }
}
