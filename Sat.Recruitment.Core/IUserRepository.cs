using Sat.Recruitment.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Core
{
    public interface IUserRepository
    {
        IAsyncEnumerable<User> GetUsersAsync();

        Task CreateUserAsync(User user);
    }
}
