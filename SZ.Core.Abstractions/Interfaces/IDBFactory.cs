using SZ.Core.Models.Db;

namespace SZ.Core.Abstractions.Interfaces
{
    public interface IDBFactory
    {
        /// <summary>
        /// Возвращает контекст БД. Если ещё не создан, то создаёт
        /// </summary>
        /// <returns></returns>
        SZDb DB { get; }
    }
}
