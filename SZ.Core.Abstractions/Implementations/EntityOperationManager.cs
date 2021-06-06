using Al;

using Microsoft.EntityFrameworkCore;

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

        public EntityOperationManager(IEntityOperationManager<T, TModel, TResult, TDB>.EventGenericHandler prepare,
            IEntityOperationManager<T, TModel, TResult, TDB>.DBActionHandler dbAction)
        {
            Prepare = prepare;
            DBAction = dbAction;
        }

        public async Task OperationAsync(
            [NotNull] TResult result,
            [NotNull] IDBProvider<TDB> provider,
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
                    await ValidRight(result, provider, model, cancellationToken);

                if (!result.Success)
                    return;

                if (ValidModel != null)
                    await ValidModel(result, provider, model, cancellationToken);

                if (!result.Success)
                    return;

                var entity = await Prepare(result, provider, model, cancellationToken);

                if (!result.Success)
                    return;

                if (entity == null)
                {
                    result.AddError("Ошибка операции с сущностью. Попробуйте снова или обратитесь к администратору",
                        "Метод подготовки сущность отработал успешно, но сущность null", 2);
                    return;
                }

                await DBAction(result, provider, entity, cancellationToken);

                if (!result.Success)
                    return;

                if (Post != null)
                    await Post(result, provider, model, cancellationToken);

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
