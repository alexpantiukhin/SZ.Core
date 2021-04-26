using Al;

using System.Threading.Tasks;

using SZ.Core.Models.Db;

namespace SZ.Core.Abstractions.Interfaces
{
    public interface IZemstvaManager
    {
        Task<Result<Zemstvo>> CreateAsync(ISZScopeEnvironment scopeEnvironment, string zemstvoName, SZDb db = null);
    }
}
