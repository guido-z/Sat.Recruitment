using Sat.Recruitment.Core;
using Sat.Recruitment.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        public async Task CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<User> GetUsersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
