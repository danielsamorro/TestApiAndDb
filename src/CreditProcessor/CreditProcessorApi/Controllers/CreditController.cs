using CreditProcessor.Domain.Commands.Requests;
using CreditProcessor.Domain.Responses;
using CreditProcessorApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CreditProcessorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CreditController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("RequestCredit")]
        [HttpPost]
        public async Task<IActionResult> RequestCredit(ProcessCreditRequest request)
        {
            Response response;

            if (request == null)
                response = new BadRequestResponse("Request cannot be null.");
            else
                response = await _mediator.Send(request);

            return response.ToActionResult();
        }
    }
}
