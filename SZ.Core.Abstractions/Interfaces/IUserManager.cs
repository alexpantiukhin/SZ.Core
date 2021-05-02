using Al;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using SZ.Core.Models;
using SZ.Core.Models.Db;

namespace SZ.Core.Abstractions.Interfaces
{
    public interface IUserManager
    {
        /// <summary>
        /// Возвращает текущего пользователя
        /// </summary>
        /// <param name="provider">провайдер БД</param>
        /// <param name="userSessionService">окружение, содержащее текущий Identity</param>
        /// <returns></returns>
        Task<User> GetCurrentUserAsync([NotNull] DBProvider provider, [NotNull] IUserSessionService userSessionService);
        /// <summary>
        /// Проверяет, является ли пользователь админом системы
        /// </summary>
        /// <param name="provider">Провайдер бд</param>
        /// <param name="userId">Id проверяемого пользователя</param>
        /// <returns></returns>
        Task<bool> IsAdminAsync([NotNull] DBProvider provider, Guid userId);
        /// <summary>
        /// Генерирует новый пароль пользователю
        /// </summary>
        /// <param name="provider">Провайдер БД</param>
        /// <param name="userSessionService">Окружение с текущим пользователем</param>
        /// <param name="userId">Id пользователя, которому генерируется новый пароль</param>
        /// <returns>Новый пароль</returns>
        Task<Result<string>> GeneratePasswordAsync([NotNull] DBProvider provider, [NotNull] IUserSessionService userSessionService, Guid userId);
    }
}
