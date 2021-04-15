using Al;

using System;
using System.Threading.Tasks;

using SZ.Core.Models.Db;

namespace SZ.Core.Abstractions.Interfaces
{
    public interface IUserManager
    {
        Task<User> GetCurrentUserAsync(IDBFactory dbFactory);
        Task<bool> IsAdminAsync(IDBFactory dbFactory, Guid userId);
        Task<Result<string>> ChangePasswordAsync(IDBFactory dbFactory, Guid userId);
    }
}
