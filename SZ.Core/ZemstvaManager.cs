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
        readonly IUserSessionService _userSessionService;

        public IEntityOperationManager<Zemstvo, string, Result<Zemstvo>, SZDb> Creator { get; }
        public IEntityOperationManager<Zemstvo, Zemstvo, Result<Zemstvo>, SZDb> Updater { get; }
        public IEntityOperationManager<Zemstvo, Guid, Result, SZDb> Deleter { get; }

        public ZemstvaManager(IUserManager userManager, IUserSessionService userSessionService, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory?.CreateLogger<ZemstvaManager>();
            _userSessionService = userSessionService;

            Creator = new EntityOperationManager<Zemstvo, string, Result<Zemstvo>, SZDb>(PrepareCreateAsync, CreateAsync)
            {
                ValidModel = ValidCreateModelAsync,
                ValidRight = ValidCreateRightAsync,
            };

            Updater = new EntityOperationManager<Zemstvo, Zemstvo, Result<Zemstvo>, SZDb>(PrepareUpdateAsync, UpdateAsync)
            {
                ValidModel = ValidUpdateModelAsync,
                ValidRight = ValidUpdateRightAsync,
            };
            //Deleter = new EntityOperationManager<Zemstvo, Guid, Result, SZDb>()
            //{

            //};

            _userManager = userManager;
        }

        #region Crate
        Task CreateAsync(Result<Zemstvo> result, IDBProvider<SZDb> dBProvider, Zemstvo entity,
            CancellationToken cancellationToken = default)
        {
            return dBProvider.DB.AddEntityAsync(entity, cancellationToken);
        }
        Task<Zemstvo> PrepareCreateAsync(Result<Zemstvo> result, IDBProvider<SZDb> dBProvider, string name,
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

            return Task.FromResult(newZemstvo);
        }

        async Task ValidCreateRightAsync(Result<Zemstvo> result, IDBProvider<SZDb> dBProvider, string zemstvoName,
            CancellationToken cancellationToken)
        {
            var currentUser = await _userManager.GetCurrentUserAsync(dBProvider, _userSessionService);

            if (currentUser == null)
            {
                result.AddError("Текущий пользователь не определён", null, 100);
                return;
            }

            if (!await _userManager.IsAdminAsync(dBProvider, currentUser.Id))
            {
                result.AddError("Только админ может создавать земства",
                    $"Попытка создать или редактировать земство {zemstvoName} пользователем {currentUser.UserName}, не имея на то прав",
                101, LogLevel.Error);
                return;
            }
        }

        Task ValidCreateModelAsync(Result<Zemstvo> result, IDBProvider<SZDb> dBProvider, string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(name))
                result.AddError("Не передано имя земства");

            return Task.CompletedTask;
        }

        #endregion

        #region Update
        Task<Zemstvo> PrepareUpdateAsync(Result<Zemstvo> result, IDBProvider<SZDb> dBProvider, Zemstvo model,
            CancellationToken cancellationToken)
        {
            dBProvider.DB.Zemstvos.Attach(model);

            return Task.FromResult(model);
        }

        async Task ValidUpdateRightAsync(Result<Zemstvo> result, IDBProvider<SZDb> dBProvider, Zemstvo model,
            CancellationToken cancellationToken)
        {
            await ValidCreateRightAsync(result, dBProvider, model.Id.ToString(), cancellationToken);

            if (!result.Success)
                return;
        }

        async Task ValidUpdateModelAsync(Result<Zemstvo> result, IDBProvider<SZDb> dBProvider, Zemstvo model, CancellationToken cancellationToken)
        {
            await ValidCreateModelAsync(result, dBProvider, model.Name, cancellationToken);

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
        Task UpdateAsync(Result<Zemstvo> result, IDBProvider<SZDb> dBProvider, Zemstvo entity,
            CancellationToken cancellationToken = default)
        {
            dBProvider.DB.Zemstvos.Update(entity);

            return Task.CompletedTask;
        }
        #endregion

        public async Task<Result> DeleteAsync([NotNull] IDBProvider<SZDb> provider, [NotNull] IUserSessionService userSessionService, [NotNull] Guid id)
        {
            Result result = new Result(_logger);
            Zemstvo zemstvo = null;

            try
            {
                zemstvo = await provider.DB.Zemstvos.FindAsync(id);

                if (zemstvo != null)
                {
                    var a = provider.DB.Zemstvos.Remove(zemstvo);

                    var deleteResult = await provider.DB.SaveChangesAsync();

                    if (deleteResult == 0)
                        return result.AddError("Земство не удалено", "При удалении земства в базе не произошло изменений");
                }

                return result.AddSuccess("Земство удалено", null, LogLevel.Information);
            }
            catch (Exception e)
            {
                return result.AddError(e, "Ошибка удаления земства");
            }
        }

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
