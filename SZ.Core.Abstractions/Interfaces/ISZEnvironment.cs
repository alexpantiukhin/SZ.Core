using Microsoft.AspNetCore.Identity;

using System.Security.Principal;
using System.Threading.Tasks;

using SZ.Core.Models.Db;

namespace SZ.Core.Abstractions.Interfaces
{
    public interface ISZEnvironment
    {
        Task<IIdentity> GetCurrentUserIdentityAsync();
        PasswordHasher<User> PasswordHasher { get; }
    }
}
