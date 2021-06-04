using Al;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using SZ.Core.Models;
using SZ.Core.Models.Interfaces;

namespace SZ.Core.Abstractions.Interfaces
{
    interface ICreateEntityManager<T, TCreate>
        where T : IDBEntity
    {
        /// <summary>
        /// Валидация модели для создания сущности
        /// </summary>
        /// <param name="result">Модель результата</param>
        /// <param name="dBProvider">Провайдер базы данных</param>
        /// <param name="model">Модель создания</param>
        /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
        Task ValidCreateModel(Result<T> result, DBProvider dBProvider, TCreate model, CancellationToken cancellationToken);
        /// <summary>
        /// Валидация прав для создания сущности
        /// </summary>
        /// <param name="result">Модель результата</param>
        /// <param name="dBProvider">Провайдер базы данных</param>
        /// <param name="model">Модель создания</param>
        /// <param name="userSessionService">Сервис сессии текущего пользователя</param>
        /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
        Task ValidCreateRight(Result<T> result, DBProvider dBProvider, TCreate model, IUserSessionService userSessionService, CancellationToken cancellationToken);
        /// <summary>
        /// Подготовливает модель для сохранения в БД
        /// </summary>
        /// <param name="result">Модель результата</param>
        /// <param name="dBProvider">Провайдер базы данных</param>
        /// <param name="model">Модель создания</param>
        /// <param name="userSessionService">Сервис сессии текущего пользователя</param>
        /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
        /// <returns>Модель, подготовленная для сохранения в БД</returns>
        Task<T> PrepareCreate(Result<T> result, DBProvider dBProvider, TCreate model, IUserSessionService userSessionService, CancellationToken cancellationToken);
    }
}
