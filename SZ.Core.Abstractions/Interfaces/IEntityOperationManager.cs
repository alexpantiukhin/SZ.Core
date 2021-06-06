using Al;

using Microsoft.EntityFrameworkCore;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace SZ.Core.Abstractions.Interfaces
{
    /// <summary>
    /// Менеджер одной из crud операций
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    /// <typeparam name="TModel">Модель операции с сущностью</typeparam>
    /// <typeparam name="TResult">Модель результата</typeparam>
    /// <typeparam name="TDB">Контекст базы данных</typeparam>
    public interface IEntityOperationManager<T, TModel, TResult, TDB>
        where T : class
        where TResult : Result
        where TDB : DbContext
    {
        /// <summary>
        /// Одно из действий операции с сущностью
        /// </summary>
        /// <param name="result">Модель результата</param>
        /// <param name="dBProvider">Провайдер БД</param>
        /// <param name="model">Модель операции с сущностью</param>
        /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
        public delegate Task EventHandler(TResult result, IDBProvider<TDB> dBProvider, TModel model,
            CancellationToken cancellationToken = default);
        /// <summary>
        /// Одно из действий операции с сущностью с типизированным результатом
        /// </summary>
        /// <param name="result">Модель результата</param>
        /// <param name="dBProvider">Провайдер БД</param>
        /// <param name="model">Модель операции с сущностью</param>
        /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
        /// <returns>Сущность</returns>
        public delegate Task<T> EventGenericHandler(TResult result, IDBProvider<TDB> dBProvider, TModel model,
            CancellationToken cancellationToken = default);
        /// <summary>
        /// Операция с базой данных для сущности
        /// </summary>
        /// <param name="result">Модель результата</param>
        /// <param name="dBProvider">Провайдер БД</param>
        /// <param name="model">Модель операции с сущностью</param>
        /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
        public delegate Task DBActionHandler(TResult result, IDBProvider<TDB> dBProvider, T entity,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Валидация модели для операции с сущностью. Ошибки с 200-299
        /// </summary>
        EventHandler ValidModel { get; init; }
        /// <summary>
        /// Валидация прав для операции с сущностью. Коды ошибок 100-199
        /// </summary>
        EventHandler ValidRight { get; init; }
        /// <summary>
        /// Подготовливает сущность из моедли для операции с сущностью. Коды ошибок 300-399
        /// </summary>
        EventGenericHandler Prepare { get; init; }
        /// <summary>
        /// Действие после операции с сущностью. Коды ошибок 400-499
        /// </summary>
        EventHandler Post { get; init; }
        /// <summary>
        /// Операция с баздой данный для сущности. Коды ошибок 500-599
        /// </summary>
        public DBActionHandler DBAction { get; init; }

        /// <summary>
        /// Операция с сущностью
        /// </summary>
        /// <param name="result">Модель результата</param>
        /// <param name="dBProvider">Провайдер БД</param>
        /// <param name="model">Модель операции с сущностью</param>
        /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
        Task OperationAsync([NotNull] TResult result, [NotNull] IDBProvider<TDB> provider,
            [NotNull] TModel model, CancellationToken cancellationToken = default);
    }
}
