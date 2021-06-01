using Al;

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
    }
}
