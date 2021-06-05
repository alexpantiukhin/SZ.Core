using Al;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using SZ.Core.Models;
using SZ.Core.Models.Interfaces;

namespace SZ.Core.Abstractions.Interfaces
{
    public interface ICRUDManager<T, TCreate, TUpdate, TDelete>
        where T : class, IDBEntity
    {
        Task<Result<T>> CreateAsync(ICreateEntityManager<T, TCreate> entityCreator, [NotNull] DBProvider provider, [NotNull] IUserSessionService userSessionService,
            [NotNull] TCreate model, CancellationToken cancellationToken = default);
        Task<Result<T>> UpdateAsync([NotNull] DBProvider provider, [NotNull] IUserSessionService userSessionService,
            [NotNull] TUpdate model, CancellationToken cancellationToken = default);
        Task<Result> DeleteAsync([NotNull] DBProvider provider, [NotNull] IUserSessionService userSessionService,
            [NotNull] TDelete model, CancellationToken cancellationToken = default);
    }
}
