using Al;

using System;
using System.Threading.Tasks;

using SZ.Core.Models.Db;

namespace SZ.Core.Abstractions.Interfaces
{
    public interface IUserManager
    {
        Task<User> GetCurrentUserAsync(ISZScopeEnvironment userSessionEnvironment, SZDb db = null);
        Task<bool> IsAdminAsync(Guid userId, SZDb db = null);
        Task<Result<string>> GeneratePasswordAsync(ISZScopeEnvironment userSessionEnvironment, Guid userId, SZDb db = null);
    }
}
