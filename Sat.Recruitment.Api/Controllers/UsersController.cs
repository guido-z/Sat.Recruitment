using Microsoft.AspNetCore.Mvc;
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

        public UsersController(IUserApplication application)
        {
            this.application = application;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserViewModel model, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return ResultFactory.FromValidationErrors(ModelState);
            }

            User user = model.MapToDomainUser();

            try
            {
                await application.CreateUserAsync(user, cancellationToken);
                return ResultFactory.FromSuccess(user);
            }
            catch (DuplicateUserException ex)
            {
                return ResultFactory.FromErrorMessages(ex.Message);
            }
        }
    }
}
