using CreditProcessor.Domain.Commands.Requests;
using CreditProcessor.Domain.Responses;
using CreditProcessorApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace CreditProcessor.UnitTests.Api.Controllers
{
    public class CreditControllerTests
    {
        private readonly MockRepository _mockRepository;
        private readonly Mock<IMediator> _mockMediator;
        private readonly CreditController _creditController;

        public CreditControllerTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _mockMediator = _mockRepository.Create<IMediator>();
            _creditController = new CreditController(_mockMediator.Object);
        }

        [Fact]
        public async Task RequestCredit_Should_Return_Ok()
        {
            var request = new ProcessCreditRequest();

            _mockMediator.Setup(m => m.Send(It.IsAny<ProcessCreditRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response("Ok"));

            var response = await _creditController.RequestCredit(request);

            _mockRepository.VerifyAll();

            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal((int)HttpStatusCode.OK, ((OkObjectResult)response).StatusCode.Value);
        }

        [Fact]
        public async Task RequestCredit_Should_Return_BadRequest()
        {
            var response = await _creditController.RequestCredit(null);

            _mockRepository.VerifyAll();

            Assert.NotNull(response);
            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal((int)HttpStatusCode.BadRequest, ((BadRequestObjectResult)response).StatusCode.Value);
        }
    }
}
