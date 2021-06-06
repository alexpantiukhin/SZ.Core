using Al;

using Microsoft.EntityFrameworkCore;

namespace SZ.Core.Abstractions.Interfaces
{
    public interface IEntityManagerCreator<T, TModel, TDb>
        where T : class
        where TDb : DbContext
    {
        IEntityOperationManager<T, TModel, Result<T>, TDb> Creator { get; }
    }
}
