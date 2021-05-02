using System.Security.Principal;
using System.Threading.Tasks;

namespace SZ.Core.Abstractions.Interfaces
{
    public interface IUserSessionService
    {
        Task<IIdentity> GetCurrentUserIdentityAsync();
    }
}
