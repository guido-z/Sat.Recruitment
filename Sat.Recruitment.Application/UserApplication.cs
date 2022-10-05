using Microsoft.Extensions.Logging;
using Sat.Recruitment.Application.Exceptions;
using Sat.Recruitment.Core;
using Sat.Recruitment.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserRepository repository;
        private readonly ILogger<UserApplication> logger;

        public UserApplication(IUserRepository repository,
            ILogger<UserApplication> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<User> CreateUserAsync(User user, CancellationToken cancellationToken = default)
        {
            var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            
            var users = repository.GetUsersAsync(cancellationToken);

            await foreach (var u in users)
            {
                if (u.Equals(user))
                {
                    cts.Cancel();
                    logger.LogError("User {0} already exists.", user.Email);
                    throw new DuplicateUserException(user);
                }
            }

            await repository.CreateUserAsync(user);
            logger.LogInformation("User {0} has been created.", user.Email);
            return user;
        }
    }
}
