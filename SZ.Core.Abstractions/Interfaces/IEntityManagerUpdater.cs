using Al;

using Microsoft.EntityFrameworkCore;

namespace SZ.Core.Abstractions.Interfaces
{
    public interface IEntityManagerUpdater<T, TModel, TDb>
        where T : class
        where TDb : DbContext
    {
        IEntityOperationManager<T, TModel, Result<T>, TDb> Updater { get; }
    }
}
