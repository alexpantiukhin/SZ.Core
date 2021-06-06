using Al;

using Microsoft.EntityFrameworkCore;

namespace SZ.Core.Abstractions.Interfaces
{
    public interface IEntityManagerDeleter<T, TModel, TDb>
        where T : class
        where TDb : DbContext
    {
        IEntityOperationManager<T, TModel, Result, TDb> Deleter { get; }
    }
}
