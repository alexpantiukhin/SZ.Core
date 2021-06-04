using Al;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

using SZ.Core.Models;
using SZ.Core.Models.Db;

namespace SZ.Core.Abstractions.Interfaces
{
    public interface IZemstvaManager
    {
        /// <summary>
        /// Создаёт земство
        /// </summary>
        /// <param name="provider">провайдер бд</param>
        /// <param name="userSessionService">окружение с текущим пользователем</param>
        /// <param name="zemstvoName">Название земства</param>
        /// <returns>Созданное земство</returns>
        Task<Result<Zemstvo>> CreateAsync([NotNull] DBProvider provider, [NotNull] IUserSessionService userSessionService,[NotNull] string zemstvoName);
        /// <summary>
        /// Валидация модели создания земства
        /// </summary>
        /// <param name="result">Изменяемый результат</param>
        /// <param name="zemstvoName">Название земства</param>
        /// <returns>Созданное земство</returns>
        void ValidCreateAsync(in Result<Zemstvo> result, [NotNull] string zemstvoName);
        /// <summary>
        /// Изменяет земство
        /// </summary>
        /// <param name="provider">провайдер бд</param>
        /// <param name="userSessionService">окружение с текущим пользователем</param>
        /// <param name="zemstvo">Модель земства</param>
        /// <returns>Созданное земство</returns>
        Task<Result<Zemstvo>> UpdateAsync([NotNull] DBProvider provider, [NotNull] IUserSessionService userSessionService, [NotNull] Zemstvo model);
        /// <summary>
        /// Удаляет земство
        /// </summary>
        /// <param name="provider">провайдер бд</param>
        /// <param name="userSessionService">окружение с текущим пользователем</param>
        /// <param name="id">id земства</param>
        /// <returns>Созданное земство</returns>
        Task<Result> DeleteAsync([NotNull] DBProvider provider, [NotNull] IUserSessionService userSessionService, [NotNull] Guid id);
        /// <summary>
        /// Возвращает земство по showId
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="userSessionService"></param>
        /// <returns></returns>
        Task<Zemstvo> GetZemstvoByShowId([NotNull] DBProvider provider, [NotNull] IUserSessionService userSessionService, int showId);

        /// <summary>
        /// Возвращает земства, в которых в данный момент состоит пользователь
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="userSessionService"></param>
        /// <returns></returns>
        Task<IQueryable<Zemstvo>> GetUserZemstvaAsync([NotNull] DBProvider provider, [NotNull] IUserSessionService userSessionService);

        Task<IQueryable<Zemstvo>> GetAllZemstvaAsync([NotNull] DBProvider provider, IUserSessionService userSessionService);
    }
}
