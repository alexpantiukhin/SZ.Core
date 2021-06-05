using Al;

using System.Threading;
using System.Threading.Tasks;

using SZ.Core.Models;
using SZ.Core.Models.Interfaces;

namespace SZ.Core.Abstractions.Interfaces
{
    public interface ICreateEntityManager<T, TCreate>
        where T : IDBEntity
    {
        /// <summary>
        /// Валидация модели для создания сущности. Ошибки с 100-199
        /// </summary>
        /// <param name="result">Модель результата</param>
        /// <param name="dBProvider">Провайдер базы данных</param>
        /// <param name="model">Модель создания</param>
        /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
        Task ValidCreateModel(Result<T> result, DBProvider dBProvider, TCreate model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Валидация прав для создания сущности. Коды ошибок 200-299
        /// </summary>
        /// <param name="result">Модель результата</param>
        /// <param name="dBProvider">Провайдер базы данных</param>
        /// <param name="model">Модель создания</param>
        /// <param name="userSessionService">Сервис сессии текущего пользователя</param>
        /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
        Task ValidCreateRight(Result<T> result, DBProvider dBProvider, TCreate model, IUserSessionService userSessionService, CancellationToken cancellationToken = default);

        /// <summary>
        /// Подготовливает модель для сохранения в БД. Коды ошибок 300-399
        /// </summary>
        /// <param name="result">Модель результата</param>
        /// <param name="dBProvider">Провайдер базы данных</param>
        /// <param name="model">Модель создания</param>
        /// <param name="userSessionService">Сервис сессии текущего пользователя</param>
        /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
        /// <returns>Модель, подготовленная для сохранения в БД</returns>
        Task<T> PrepareCreate(Result<T> result, DBProvider dBProvider, TCreate model, IUserSessionService userSessionService, CancellationToken cancellationToken = default);

        /// <summary>
        /// Операция подготовки создания сущности. Коды ошибок 400-499
        /// </summary>
        /// <param name="result">Модель результата</param>
        /// <param name="dBProvider">Провайдер базы данных</param>
        /// <param name="model">Модель создания</param>
        /// <param name="userSessionService">Сервис сессии текущего пользователя</param>
        /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
        Task PostCreate(Result<T> result, DBProvider dBProvider, TCreate model, IUserSessionService userSessionService, CancellationToken cancellationToken = default);
    }
}
