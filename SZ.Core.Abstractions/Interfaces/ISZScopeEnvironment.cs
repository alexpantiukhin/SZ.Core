using System.Security.Principal;
using System.Threading.Tasks;

namespace SZ.Core.Abstractions.Interfaces
{
    public interface ISZScopeEnvironment
    {
        Task<IIdentity> GetCurrentUserIdentityAsync();
    }
}
