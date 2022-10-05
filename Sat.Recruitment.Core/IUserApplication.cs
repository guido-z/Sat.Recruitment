using Sat.Recruitment.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Core
{
    public interface IUserApplication
    {
        Task<User> CreateUserAsync(User user, CancellationToken cancellationToken);
    }
}
