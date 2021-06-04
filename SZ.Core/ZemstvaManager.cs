using Al;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

using SZ.Core.Abstractions.Interfaces;
using SZ.Core.Models;
using SZ.Core.Models.Db;

namespace SZ.Core
{
    public class ZemstvaManager : IZemstvaManager
    {
        ICRUDManager<Zemstvo, string, object, object> CRUDManager;
        readonly IUserManager _userManager;
        readonly ILogger _logger;
        public ZemstvaManager(IUserManager userManager, ILoggerFactory loggerFactory = null)
        {
            CRUDManager = new CRUDManager<Zemstvo, string, object, object>(loggerFactory)
            {
                
            };
            _userManager = userManager;
            _logger = loggerFactory?.CreateLogger<ZemstvaManager>();
        }

        public async Task<Result<Zemstvo>> CreateAsync([NotNull] DBProvider provider, [NotNull] IUserSessionService userSessionService, [NotNull] string zemstvoName)
        {
            var result = new Result<Zemstvo>(_logger);

            var currentUser = await _userManager.GetCurrentUserAsync(provider, userSessionService);

            if (currentUser == null)
                return result.AddError("Текущий пользователь не определён");

            if (!await _userManager.IsAdminAsync(provider, currentUser.Id))
                return result.AddError("Только админ может создавать земства");

            ValidCreateAsync(result, zemstvoName);

            if (!result.Success)
                return result;

            var newZemstvo = new Zemstvo
            {
                Id = Guid.NewGuid(),
                Name = zemstvoName,
                AutoConfirmProtocolCircle = 1,
                QuorumMeetingTen = 2 / 3,
                QuorumTensForQuestion = 2 / 3,
                RequirePaperCircle = 1,
                QuorumVotingTen = 2 / 3
            };

            var addedResult = await provider.DB.AddEntityAsync(newZemstvo);

            if (!addedResult.Success)
                return result.AddError("Ошибка создания земства");

            return result.AddModel(newZemstvo, $"Земство {newZemstvo.Name} создано");
        }
        public void ValidCreateAsync([NotNull] in Result<Zemstvo> result, [NotNull] string zemstvoName)
        {
            if (string.IsNullOrWhiteSpace(zemstvoName))
                result.AddError("Не передано имя земства");
        }
        public async Task<Result<Zemstvo>> UpdateAsync([NotNull] DBProvider provider, [NotNull] IUserSessionService userSessionService, [NotNull] Zemstvo model)
        {
            Result<Zemstvo> result = new Result<Zemstvo>(_logger);
            try
            {
                var zemstvo = await provider.DB.Zemstvos.FindAsync(model.Id);

                if (zemstvo == null)
                    return result.AddError($"Земства с id {model.ShowId} не существует");

                zemstvo.Name = model.Name;
                zemstvo.QuorumVotingTen = model.QuorumVotingTen;
                zemstvo.RequirePaperCircle = model.RequirePaperCircle;
                zemstvo.AutoConfirmProtocolCircle = model.AutoConfirmProtocolCircle;
                zemstvo.QuorumMeetingTen = model.QuorumMeetingTen;
                zemstvo.QuorumTensForQuestion = model.QuorumTensForQuestion;

                var saveResult = await provider.DB.SaveChangesAsync();

                return result.AddModel(zemstvo, "Изменения земства сохранены");
            }
            catch (Exception e)
            {
                return result.AddError(e, "Ошибка изменения земства");
            }
        }
        
        public async Task<Result> DeleteAsync([NotNull] DBProvider provider, [NotNull] IUserSessionService userSessionService, [NotNull] Guid id)
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

        public async Task<IQueryable<Zemstvo>> GetUserZemstvaAsync([NotNull] DBProvider provider, [NotNull] IUserSessionService userSessionService)
        {
            var currentUser = await _userManager.GetCurrentUserAsync(provider, userSessionService);

            if (currentUser == null)
                return null;


            //TODO проверку на повторяющиеся земства, если в нескольких десятках состоит
            return provider.DB.UserTens
                .Where(x => x.UserId == currentUser.Id && x.Ten.Circle == 1 && x.BasisExitDocumentId == null)
                .Select(x => x.Ten.Zemstvo).Distinct();
        }

        public async Task<IQueryable<Zemstvo>> GetAllZemstvaAsync([NotNull] DBProvider provider, IUserSessionService userSessionService)
        {
            var currentUser = await _userManager.GetCurrentUserAsync(provider, userSessionService);

            if (currentUser == null)
                return null;

            return provider.DB.Zemstvos;
        }

    }
}
