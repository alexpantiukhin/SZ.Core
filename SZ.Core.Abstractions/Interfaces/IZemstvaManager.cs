using Al;

using System.Diagnostics.CodeAnalysis;
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
        /// <param name="scopeEnvironment">окружение с текущим пользователем</param>
        /// <param name="zemstvoName">Название земства</param>
        /// <returns>Созданное земство</returns>
        Task<Result<Zemstvo>> CreateAsync([NotNull] DBProvider provider, [NotNull] ISZScopeEnvironment scopeEnvironment,[NotNull] string zemstvoName);
    }
}
