using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

using SZ.Core.Models.Db;

namespace SZ.Core.Abstractions.Interfaces
{
    public interface IZemstvaManager :
        IEntityManagerCreator<Zemstvo, string, SZDb>,
        IEntityManagerUpdater<Zemstvo, Zemstvo, SZDb>,
        IEntityManagerDeleter<Zemstvo, Guid, SZDb>
    {
        /// <summary>
        /// Возвращает земства, в которых в данный момент состоит пользователь
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="userSessionService"></param>
        /// <returns></returns>
        Task<IQueryable<Zemstvo>> GetUserZemstvaAsync([NotNull] IDBProvider<SZDb> provider, [NotNull] IUserSessionService userSessionService);

        Task<IQueryable<Zemstvo>> GetAllZemstvaAsync([NotNull] IDBProvider<SZDb> provider, IUserSessionService userSessionService);
    }
}
