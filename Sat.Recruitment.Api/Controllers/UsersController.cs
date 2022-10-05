using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Mappers;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Results;
using Sat.Recruitment.Application.Exceptions;
using Sat.Recruitment.Core;
using Sat.Recruitment.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IUserApplication application;
        private readonly ILogger<UsersController> logger;

        public UsersController(IUserApplication application,
            ILogger<UsersController> logger)
        {
            this.application = application;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(UserViewModel model, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Creating user {0}.", model.Email);

            if (!ModelState.IsValid)
            {
                logger.LogError("Validation failed for user {0}.", model.Email);
                return ResultFactory.FromValidationErrors(ModelState);
            }

            User user = model.MapToDomainUser();

            try
            {
                await application.CreateUserAsync(user, cancellationToken);
                logger.LogInformation("User {0} has been successfully created.", model.Email);
                return ResultFactory.FromSuccess(user);
            }
            catch (DuplicateUserException ex)
            {
                logger.LogError("User {0} is duplicated.", model.Email);
                return ResultFactory.FromErrorMessages(ex.Message);
            }
        }
    }
}
