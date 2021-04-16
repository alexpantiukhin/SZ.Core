using Al;

using System;
using System.Threading.Tasks;

using SZ.Core.Models.Db;

namespace SZ.Core.Abstractions.Interfaces
{
    public interface IUserManager
    {
        Task<User> GetCurrentUserAsync(SZDb db = null);
        Task<bool> IsAdminAsync(Guid userId, SZDb db = null);
        Task<Result<string>> ChangePasswordAsync(Guid userId, SZDb db = null);
    }
}
