using Al;

using System.Threading;
using System.Threading.Tasks;

using SZ.Core.Models;
using SZ.Core.Models.Interfaces;

namespace SZ.Core.Abstractions.Interfaces
{
    public interface IDeleteEntityManager<T, TDelete>
        where T : IDBEntity
    {
        /// <summary>
        /// Валидация модели для удаления сущности. Ошибки с 100-199
        /// </summary>
        /// <param name="result">Модель результата</param>
        /// <param name="dBProvider">Провайдер базы данных</param>
        /// <param name="model">Модель изменения</param>
        /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
        Task ValidDeleteModel(Result<T> result, DBProvider dBProvider, TDelete model, IUserSessionService userSessionService, CancellationToken cancellationToken = default);

        /// <summary>
        /// Валидация прав для удаления сущности. Коды ошибок 200-299
        /// </summary>
        /// <param name="result">Модель результата</param>
        /// <param name="dBProvider">Провайдер базы данных</param>
        /// <param name="model">Модель изменения</param>
        /// <param name="userSessionService">Сервис сессии текущего пользователя</param>
        /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
        Task ValidDeleteRight(Result<T> result, DBProvider dBProvider, TDelete model, IUserSessionService userSessionService, CancellationToken cancellationToken = default);

        /// <summary>
        /// Подготовливает модель для удаления из БД. Коды ошибок 300-399
        /// </summary>
        /// <param name="result">Модель результата</param>
        /// <param name="dBProvider">Провайдер базы данных</param>
        /// <param name="model">Модель изменения</param>
        /// <param name="userSessionService">Сервис сессии текущего пользователя</param>
        /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
        /// <returns>Модель, подготовленная для удаления из БД</returns>
        Task<T> PrepareDelete(Result<T> result, DBProvider dBProvider, TDelete model, IUserSessionService userSessionService, CancellationToken cancellationToken = default);

        /// <summary>
        /// Действие после удаления сущности. Коды ошибок 400-499
        /// </summary>
        /// <param name="result">Модель результата</param>
        /// <param name="dBProvider">Провайдер базы данных</param>
        /// <param name="model">Модель удаления</param>
        /// <param name="userSessionService">Сервис сессии текущего пользователя</param>
        /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
        Task PostDelete(Result<T> result, DBProvider dBProvider, TDelete model, IUserSessionService userSessionService, CancellationToken cancellationToken = default);
    }
}
