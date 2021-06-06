using Al;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using SZ.Core.Abstractions.Interfaces;
using SZ.Core.Models;
using SZ.Core.Models.Interfaces;

namespace SZ.Core
{
    public class CRUDManager<T, TModel, TResult> : ICRUDManager<T, TModel, TResult>
        where T : class, IDBEntity
        where TResult : Result
    {
        public ICRUDManager<T, TModel, TResult>.CRUDEventHandler ValidModel { get; }
        public ICRUDManager<T, TModel, TResult>.CRUDEventHandler ValidRight { get; }
        public ICRUDManager<T, TModel, TResult>.CRUDEventGenericHandler Prepare { get; }
        public ICRUDManager<T, TModel, TResult>.CRUDEventHandler Post { get; }
        public ICRUDManager<T, TModel, TResult>.CRUDDBActionHandler DBAction { get; }

        public async Task OperationAsync(
            [NotNull] TResult result,
            [NotNull] DBProvider provider,
            [NotNull] IUserSessionService userSessionService,
            [NotNull] TModel model,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (Prepare == null || DBAction == null)
                {
                    result.AddError("Ошибка создания записи", "Не передан метод подготовки сущности или метод запроса к БД", 1);
                    return;
                }

                if (ValidRight != null)
                    await ValidRight(result, provider, model, userSessionService, cancellationToken);

                if (!result.Success)
                    return;

                if (ValidModel != null)
                    await ValidModel(result, provider, model, userSessionService, cancellationToken);

                if (!result.Success)
                    return;

                var entity = await Prepare(result, provider, model, userSessionService, cancellationToken);

                if (!result.Success)
                    return;

                if (entity == null)
                {
                    result.AddError("Ошибка операции с сущностью. Попробуйте снова или обратитесь к администратору",
                        "Метод подготовки сущность отработал успешно, но сущность null", 2);
                    return;
                }

                await DBAction(result, provider, entity, userSessionService, cancellationToken);

                if (!result.Success)
                    return;

                if (Post != null)
                    await Post(result, provider, model, userSessionService, cancellationToken);

                if (!result.Success)
                    return;

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
        }
    }
}
