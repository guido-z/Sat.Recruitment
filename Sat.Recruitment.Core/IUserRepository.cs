using Sat.Recruitment.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Core
{
    public interface IUserRepository
    {
        IAsyncEnumerable<User> GetUsersAsync(CancellationToken cancellationToken);

        Task CreateUserAsync(User user);
    }
}
