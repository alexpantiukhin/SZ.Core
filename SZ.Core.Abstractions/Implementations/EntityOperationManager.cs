using Al;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using SZ.Core.Abstractions.Interfaces;

namespace SZ.Core.Abstractions.Implementations
{
    public class EntityOperationManager<T, TModel, TResult, TDB> : IEntityOperationManager<T, TModel, TResult, TDB>
        where T : class
        where TResult : Result
        where TDB : DbContext
    {
        public IEntityOperationManager<T, TModel, TResult, TDB>.EventHandler ValidModel { get; init; }
        public IEntityOperationManager<T, TModel, TResult, TDB>.EventHandler ValidRight { get; init; }
        public IEntityOperationManager<T, TModel, TResult, TDB>.EventGenericHandler Prepare { get; }
        public IEntityOperationManager<T, TModel, TResult, TDB>.EventHandler Post { get; init; }
        public IEntityOperationManager<T, TModel, TResult, TDB>.DBActionHandler DBAction { get; }

        readonly Func<ILogger, TResult> _resultMaker;
        readonly ILogger _logger;

        public EntityOperationManager(IEntityOperationManager<T, TModel, TResult, TDB>.EventGenericHandler prepare,
            IEntityOperationManager<T, TModel, TResult, TDB>.DBActionHandler dbAction,
            Func<ILogger, TResult> resultMaker, ILoggerFactory loggerFactory)
        {
            Prepare = prepare;
            DBAction = dbAction;
            _resultMaker = resultMaker;
            _logger = loggerFactory?.CreateLogger<EntityOperationManager<T, TModel, TResult, TDB>>();
        }

        public async ValueTask<TResult> OperationAsync(
            [NotNull] IDBProvider<TDB> provider,
            [NotNull] IUserSessionService userSessionService,
            [NotNull] TModel model,
            CancellationToken cancellationToken = default)
        {
            var result = _resultMaker(_logger);

            try
            {
                if (Prepare == null || DBAction == null)
                {
                    result.AddError("Ошибка создания записи",
                        "Не передан метод подготовки сущности или метод запроса к БД", 1);

                    return result;
                }

                if (ValidRight != null)
                    await ValidRight(result, provider, userSessionService, model, cancellationToken);

                if (!result.Success)
                    return result;

                if (ValidModel != null)
                    await ValidModel(result, provider, userSessionService, model, cancellationToken);

                if (!result.Success)
                    return result;

                var entity = await Prepare(result, provider, userSessionService, model, cancellationToken);

                if (!result.Success)
                    return result;

                if (entity == null)
                {
                    result.AddError("Ошибка операции с сущностью. Попробуйте снова или обратитесь к администратору",
                        "Метод подготовки сущность отработал успешно, но сущность null", 2);
                    return result;
                }

                await DBAction(result, provider, userSessionService, entity, cancellationToken);

                if (!result.Success)
                    return result;

                if (Post != null)
                    await Post(result, provider, userSessionService, model, cancellationToken);

                if (!result.Success)
                    return result;

                if (result is Result<T> tResult)
                    tResult.AddModel(entity, "Операция с сущностью успешна");

            }
            catch (TaskCanceledException e)
            {
                result.AddError(e, "Операция отменена", 3);
            }
            catch (Exception e)
            {
                result.AddError(e, "Ошибка операции с сущностью. Попробуйте снова", 4);
            }

            return result;
        }
    }
}
