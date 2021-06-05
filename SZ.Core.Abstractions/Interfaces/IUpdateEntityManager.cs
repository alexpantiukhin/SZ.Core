using Al;

using System.Threading;
using System.Threading.Tasks;

using SZ.Core.Models;
using SZ.Core.Models.Interfaces;

namespace SZ.Core.Abstractions.Interfaces
{
    public interface IUpdateEntityManager<T, TUpdate>
        where T : IDBEntity
    {
        /// <summary>
        /// Валидация модели для изменения сущности. Ошибки с 100-199
        /// </summary>
        /// <param name="result">Модель результата</param>
        /// <param name="dBProvider">Провайдер базы данных</param>
        /// <param name="model">Модель изменения</param>
        /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
        Task ValidUpdateModel(Result<T> result, DBProvider dBProvider, TUpdate model, IUserSessionService userSessionService, CancellationToken cancellationToken = default);

        /// <summary>
        /// Валидация прав для изменения сущности. Коды ошибок 200-299
        /// </summary>
        /// <param name="result">Модель результата</param>
        /// <param name="dBProvider">Провайдер базы данных</param>
        /// <param name="model">Модель изменения</param>
        /// <param name="userSessionService">Сервис сессии текущего пользователя</param>
        /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
        Task ValidUpdateRight(Result<T> result, DBProvider dBProvider, TUpdate model, IUserSessionService userSessionService, CancellationToken cancellationToken = default);

        /// <summary>
        /// Подготовливает модель для сохранения в БД. Коды ошибок 300-399
        /// </summary>
        /// <param name="result">Модель результата</param>
        /// <param name="dBProvider">Провайдер базы данных</param>
        /// <param name="model">Модель изменения</param>
        /// <param name="userSessionService">Сервис сессии текущего пользователя</param>
        /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
        /// <returns>Модель, подготовленная для сохранения в БД</returns>
        Task<T> PrepareUpdate(Result<T> result, DBProvider dBProvider, TUpdate model, IUserSessionService userSessionService, CancellationToken cancellationToken = default);

        /// <summary>
        /// Действие после изменения сущности. Коды ошибок 400-499
        /// </summary>
        /// <param name="result">Модель результата</param>
        /// <param name="dBProvider">Провайдер базы данных</param>
        /// <param name="model">Модель изменения</param>
        /// <param name="userSessionService">Сервис сессии текущего пользователя</param>
        /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
        Task PostUpdate(Result<T> result, DBProvider dBProvider, TUpdate model, IUserSessionService userSessionService, CancellationToken cancellationToken = default);
    }
}
