using Sat.Recruitment.Core;
using Sat.Recruitment.Domain;
using System;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserRepository userRepository;

        public UserApplication(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public Task<User> CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
