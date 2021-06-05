using Al;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using SZ.Core.Models;
using SZ.Core.Models.Interfaces;

namespace SZ.Core.Abstractions.Interfaces
{
    public interface ICRUDManager<T>
        where T : class, IDBEntity
    {
        Task<Result<T>> CreateAsync<TCreate>(ICreateEntityManager<T, TCreate> entityCreator,
            [NotNull] DBProvider provider, [NotNull] IUserSessionService userSessionService,
            [NotNull] TCreate model, CancellationToken cancellationToken = default);
        Task<Result<T>> UpdateAsync<TUpdate>(IUpdateEntityManager<T, TUpdate> entityUpdator,
            [NotNull] DBProvider provider, [NotNull] IUserSessionService userSessionService,
            [NotNull] TUpdate model, CancellationToken cancellationToken = default);
        Task<Result> DeleteAsync<TDelete>(
            [NotNull] DBProvider provider, [NotNull] IUserSessionService userSessionService,
            [NotNull] TDelete model, CancellationToken cancellationToken = default);
    }
}
