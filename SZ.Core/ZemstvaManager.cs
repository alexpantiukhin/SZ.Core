using Al;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using SZ.Core.Abstractions.Implementations;
using SZ.Core.Abstractions.Interfaces;
using SZ.Core.Models.Db;

namespace SZ.Core
{
    public class ZemstvaManager : IZemstvaManager
    {
        readonly IUserManager _userManager;
        readonly ILogger _logger;

        public IEntityOperationManager<Zemstvo, string, Result<Zemstvo>, SZDb> Creator { get; }
        public IEntityOperationManager<Zemstvo, Zemstvo, Result<Zemstvo>, SZDb> Updater { get; }
        public IEntityOperationManager<Zemstvo, Guid, Result, SZDb> Deleter { get; }

        public ZemstvaManager(IUserManager userManager, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory?.CreateLogger<ZemstvaManager>();

            Creator = new EntityOperationManager<Zemstvo, string, Result<Zemstvo>, SZDb>(
                PrepareCreateAsync,
                CreateAsync,
                x => new Result<Zemstvo>(x),
                loggerFactory)
            {
                ValidModel = ValidCreateModelAsync,
                ValidRight = ValidCreateRightAsync,
            };

            Updater = new EntityOperationManager<Zemstvo, Zemstvo, Result<Zemstvo>, SZDb>(
                PrepareUpdateAsync,
                UpdateAsync,
                x => new Result<Zemstvo>(x),
                loggerFactory)
            {
                ValidModel = ValidUpdateModelAsync,
                ValidRight = ValidUpdateRightAsync,
            };

            Deleter = new EntityOperationManager<Zemstvo, Guid, Result, SZDb>(
                PrepareDeleteAsync,
                DeleteAsync,
                x => new Result(x),
                loggerFactory)
            {
                ValidRight = ValidDeleteRightAsync
            };

            _userManager = userManager;
        }

        #region Crate
        async ValueTask CreateAsync(Result<Zemstvo> result, IDBProvider<SZDb> dBProvider,
            IUserSessionService userSessionService, Zemstvo entity,
            CancellationToken cancellationToken = default)
        {
            var createResult = await dBProvider.DB.AddEntityAsync(entity, cancellationToken);

            if (createResult.Success)
                return;

            result.AddError(createResult.UserMessage, createResult.AdminMessage, 500);
        }
        ValueTask<Zemstvo> PrepareCreateAsync(Result<Zemstvo> result, IDBProvider<SZDb> dBProvider,
            IUserSessionService userSessionService, string name,
            CancellationToken cancellationToken)
        {
            var newZemstvo = new Zemstvo
            {
                Id = Guid.NewGuid(),
                Name = name,
                AutoConfirmProtocolCircle = 1,
                QuorumMeetingTen = 2 / 3,
                QuorumTensForQuestion = 2 / 3,
                RequirePaperCircle = 1,
                QuorumVotingTen = 2 / 3
            };

            return ValueTask.FromResult(newZemstvo);
        }

        async ValueTask ValidCreateRightAsync(Result<Zemstvo> result, IDBProvider<SZDb> dBProvider,
            IUserSessionService userSessionService, string zemstvoName, CancellationToken cancellationToken)
        {
            var currentUser = await _userManager.GetCurrentUserAsync(dBProvider, userSessionService, cancellationToken);

            if (currentUser == null)
            {
                result.AddError("Текущий пользователь не определён", null, 100);
                return;
            }

            if (!await _userManager.IsAdminAsync(dBProvider, currentUser.Id, cancellationToken))
            {
                result.AddError("Только админ может создавать земства",
                    $"Попытка создать земство {zemstvoName} пользователем {currentUser.UserName}, не имея на то прав",
                101, LogLevel.Error);
                return;
            }
        }

        ValueTask ValidCreateModelAsync(Result<Zemstvo> result, IDBProvider<SZDb> dBProvider,
            IUserSessionService userSessionService, string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(name))
                result.AddError("Не передано имя земства");

            return ValueTask.CompletedTask;
        }

        #endregion

        #region Update
        ValueTask<Zemstvo> PrepareUpdateAsync(Result<Zemstvo> result, IDBProvider<SZDb> dBProvider,
            IUserSessionService userSessionService, Zemstvo model,
            CancellationToken cancellationToken)
        {
            dBProvider.DB.Zemstvos.Attach(model);

            return ValueTask.FromResult(model);
        }

        async ValueTask ValidUpdateRightAsync(Result<Zemstvo> result, IDBProvider<SZDb> dBProvider,
            IUserSessionService userSessionService, Zemstvo model,
            CancellationToken cancellationToken)
        {
            var currentUser = await _userManager.GetCurrentUserAsync(dBProvider, userSessionService, cancellationToken);

            if (currentUser == null)
            {
                result.AddError("Текущий пользователь не определён", null, 100);
                return;
            }

            if (await _userManager.IsAdminAsync(dBProvider, currentUser.Id, cancellationToken))
                return;

            //TODO проверка на роль главы земства


            result.AddError("Только админ может создавать земства",
                $"Попытка редактировать земство {model.Id} пользователем {currentUser.UserName}, не имея на то прав",
            101, LogLevel.Error);
        }

        async ValueTask ValidUpdateModelAsync(Result<Zemstvo> result, IDBProvider<SZDb> dBProvider,
            IUserSessionService userSessionService, Zemstvo model, CancellationToken cancellationToken)
        {
            await ValidCreateModelAsync(result, dBProvider, userSessionService, model.Name, cancellationToken);

            if (!result.Success)
                return;

            if (model.QuorumVotingTen <= 0)
                result.AddError("Кворум голосов в десятке указан неверно", null, 201);
            else if (model.QuorumMeetingTen <= 0)
                result.AddError("Кворум присутствующих в десятке указан неверно", null, 202);
            else if (model.QuorumTensForQuestion <= 0)
                result.AddError("Неверно указана доля поддесяток, которые должны принять решение по вопросу, прежде, чем он поступит на обсуждение в следующий круг", null, 200);
            else if (model.RequirePaperCircle == 0)
                result.AddError("Неверно указан круг, с которого бумажные протоколы становятся обязательными", null, 203);
        }
        async ValueTask UpdateAsync(Result<Zemstvo> result, IDBProvider<SZDb> dBProvider,
            IUserSessionService userSessionService, Zemstvo entity,
            CancellationToken cancellationToken = default)
        {
            dBProvider.DB.Zemstvos.Update(entity);

            await dBProvider.DB.SaveChangesAsync();
        }
        #endregion

        #region Delete
        async ValueTask<Zemstvo> PrepareDeleteAsync(Result result, IDBProvider<SZDb> dBProvider,
            IUserSessionService userSessionService, Guid model,
            CancellationToken cancellationToken)
        {
            return await dBProvider.DB.Zemstvos.FindAsync(new object[] { model }, cancellationToken);
        }

        async ValueTask ValidDeleteRightAsync(Result result, IDBProvider<SZDb> dBProvider,
            IUserSessionService userSessionService, Guid model,
            CancellationToken cancellationToken)
        {
            var validCreateResult = new Result<Zemstvo>(_logger);

            await ValidCreateModelAsync(validCreateResult, dBProvider, userSessionService, model.ToString(), cancellationToken);

            if (validCreateResult.Success)
                return;

            result.AddError(validCreateResult.UserMessage, validCreateResult.AdminMessage, validCreateResult.ErrorCode);
        }

        ValueTask DeleteAsync(Result result, IDBProvider<SZDb> dBProvider,
            IUserSessionService userSessionService, Zemstvo entity,
            CancellationToken cancellationToken = default)
        {
            dBProvider.DB.Zemstvos.Remove(entity);

            dBProvider.DB.SaveChangesAsync();

            return ValueTask.CompletedTask;
        }
        #endregion

        public Task<Zemstvo> GetZemstvoByShowId([NotNull] DBProvider provider, [NotNull] IUserSessionService userSessionService, int showId)
        {
            return provider.DB.Zemstvos.FirstOrDefaultAsync(x => x.ShowId == showId);
        }

        public async Task<IQueryable<Zemstvo>> GetUserZemstvaAsync([NotNull] IDBProvider<SZDb> provider, [NotNull] IUserSessionService userSessionService)
        {
            var currentUser = await _userManager.GetCurrentUserAsync(provider, userSessionService);

            if (currentUser == null)
                return null;


            //TODO проверку на повторяющиеся земства, если в нескольких десятках состоит
            return provider.DB.UserTens
                .Where(x => x.UserId == currentUser.Id && x.Ten.Circle == 1 && x.BasisExitDocumentId == null)
                .Select(x => x.Ten.Zemstvo).Distinct();
        }

        public async Task<IQueryable<Zemstvo>> GetAllZemstvaAsync([NotNull] IDBProvider<SZDb> provider, IUserSessionService userSessionService)
        {
            var currentUser = await _userManager.GetCurrentUserAsync(provider, userSessionService);

            if (currentUser == null)
                return null;

            return provider.DB.Zemstvos;
        }

    }
}
