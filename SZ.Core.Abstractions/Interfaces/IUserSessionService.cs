using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace SZ.Core.Abstractions.Interfaces
{
    public interface IUserSessionService
    {
        Task<IIdentity> GetCurrentUserIdentityAsync(CancellationToken cancellationToken = default);
    }
}
