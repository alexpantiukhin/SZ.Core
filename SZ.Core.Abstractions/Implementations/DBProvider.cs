using Microsoft.EntityFrameworkCore;

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using SZ.Core.Abstractions.Interfaces;
using SZ.Core.Models.Db;

namespace SZ.Core.Abstractions.Implementations
{
    public class DBProvider : IDBProvider<SZDb>
    {
        readonly IDbContextFactory<SZDb> _dbFactory;
        SZDb _db;

        public SZDb DB => _db ??= _dbFactory.CreateDbContext();

        public DBProvider([NotNull] IDbContextFactory<SZDb> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public void Dispose()
        {
            _db.Dispose();
            _db = null;
        }

        public async ValueTask DisposeAsync()
        {
            await _db.DisposeAsync();
            _db = null;
        }
    }
}
