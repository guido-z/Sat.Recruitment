using Sat.Recruitment.Application.Exceptions;
using Sat.Recruitment.Core;
using Sat.Recruitment.Domain;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserRepository repository;

        public UserApplication(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var users = repository.GetUsersAsync();

            await foreach (var u in users)
            {
                if (u.Equals(user))
                {
                    throw new DuplicateUserException(user);
                }
            }

            await repository.CreateUserAsync(user);
            return user;
        }
    }
}
