using Al;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using SZ.Core.Models;
using SZ.Core.Models.Interfaces;

namespace SZ.Core.Abstractions.Interfaces
{
    public interface ICRUDManager<T, TModel, TResult>
        where T : class, IDBEntity
        where TResult : Result
    {
        public delegate Task CRUDEventHandler(TResult result, DBProvider dBProvider, TModel model,
            IUserSessionService userSessionService, CancellationToken cancellationToken = default);
        public delegate Task<T> CRUDEventGenericHandler(TResult result, DBProvider dBProvider, TModel model,
            IUserSessionService userSessionService, CancellationToken cancellationToken = default);
        public delegate Task CRUDDBActionHandler(TResult result, DBProvider dBProvider, T entity,
                IUserSessionService userSessionService, CancellationToken cancellationToken = default);

        /// <summary>
        /// Валидация модели для операции с сущностью. Ошибки с 100-199
        /// </summary>
        /// <param name="result">Модель результата</param>
        /// <param name="dBProvider">Провайдер базы данных</param>
        /// <param name="model">Модель операции с сущностью</param>
        /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
        CRUDEventHandler ValidModel { get; }
        /// <summary>
        /// Валидация прав для операции с сущностью. Коды ошибок 200-299
        /// </summary>
        /// <param name="result">Модель результата</param>
        /// <param name="dBProvider">Провайдер базы данных</param>
        /// <param name="model">Модель операции с сущностью</param>
        /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
        CRUDEventHandler ValidRight { get; }
        /// <summary>
        /// Подготовливает сущность из моедли для операции с сущностью. Коды ошибок 300-399
        /// </summary>
        /// <param name="result">Модель результата</param>
        /// <param name="dBProvider">Провайдер базы данных</param>
        /// <param name="model">Модель операции с сущностью</param>
        /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
        CRUDEventGenericHandler Prepare { get; }
        /// <summary>
        /// Действие после операции с сущностью. Коды ошибок 400-499
        /// </summary>
        /// <param name="result">Модель результата</param>
        /// <param name="dBProvider">Провайдер базы данных</param>
        /// <param name="model">Модель операции с сущностью</param>
        /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
        CRUDEventHandler Post { get; }
        public CRUDDBActionHandler DBAction { get; }

        Task OperationAsync([NotNull] TResult result, [NotNull] DBProvider provider, [NotNull] IUserSessionService userSessionService,
            [NotNull] TModel model, CancellationToken cancellationToken = default);
    }
}
