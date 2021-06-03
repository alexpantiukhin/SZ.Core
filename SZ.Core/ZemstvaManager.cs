using Al;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SZ.Core.Abstractions.Interfaces;
using SZ.Core.Models;
using SZ.Core.Models.Db;

namespace SZ.Core
{
    public class ZemstvaManager : IZemstvaManager
    {
        readonly IUserManager _userManager;
        readonly ILogger _logger;
        public ZemstvaManager(IUserManager userManager, ILoggerFactory loggerFactory = null)
        {
            _userManager = userManager; 
            _logger = loggerFactory?.CreateLogger<ZemstvaManager>();
        }

        public async Task<Result<Zemstvo>> CreateAsync([NotNull] DBProvider provider, [NotNull] IUserSessionService userSessionService, [NotNull] string zemstvoName)
        {
            var result = new Result<Zemstvo>(_logger);

            if (string.IsNullOrWhiteSpace(zemstvoName))
                return result.AddError("Не передано имя земства");

            var currentUser = await _userManager.GetCurrentUserAsync(provider, userSessionService);

            if (currentUser == null)
                return result.AddError("Текущий пользователь не определён");

            if (!await _userManager.IsAdminAsync(provider, currentUser.Id))
                return result.AddError("Только админ может создавать земства");

            var newZemstvo = new Zemstvo
            {
                Id = Guid.NewGuid(),
                Name = zemstvoName,
                AutoConfirmProtocolCircle = 1,
                QuorumMeetingTen = 2/3,
                QuorumTensForQuestion = 2/3,
                RequirePaperCircle = 1,
                QuorumVotingTen = 2/3
            };

            var addedResult = await provider.DB.AddEntityAsync(newZemstvo);

            if (!addedResult)
                return result.AddError("Ошибка создания земства");

            return result.AddModel(newZemstvo);
        }
        public Task<Result<Zemstvo>> UpdateAsync([NotNull] DBProvider provider, [NotNull] IUserSessionService userSessionService, [NotNull] Zemstvo zemstvo)
        {
            throw new NotImplementedException();
        }


        public async Task<Zemstvo> GetZemstvoByShowId([NotNull] DBProvider provider, [NotNull] IUserSessionService userSessionService, int showId)
        {
            throw new Exception("lkdjf");
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
