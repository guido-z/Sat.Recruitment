using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Sat.Recruitment.Api.Results
{
    static class ResultFactory
    {
        public static IActionResult FromSuccess(object data = null)
        {
            return new OkObjectResult(new Result
            {
                IsSuccess = true,
                Errors = Enumerable.Empty<string>().ToArray(),
                Data = data
            });
        }
        public static IActionResult FromErrorMessages(params string[] messages)
        {
            return new BadRequestObjectResult(new Result
            {
                IsSuccess = false,
                Errors = messages
            });
        }
    }
}
