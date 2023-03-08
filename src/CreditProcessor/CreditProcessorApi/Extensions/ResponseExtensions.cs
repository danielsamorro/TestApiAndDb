using CreditProcessor.Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CreditProcessorApi.Extensions
{
    public static class ResponseExtensions
    {
        public static IActionResult ToActionResult(this Response response)
        {
            if (response == null)
                return new ObjectResult("Internal Server Error") { StatusCode = (int?)HttpStatusCode.InternalServerError };

            if (response.Errors.Any())
                if (response is BadRequestResponse)
                    return new BadRequestObjectResult(response.Errors);
                else
                    return new ObjectResult(response.Errors) { StatusCode = (int?)HttpStatusCode.InternalServerError };

            return new OkObjectResult(response.Message);
        }
    }
}
