using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using System.Threading.Tasks;

using SZ.Core.Abstractions.Interfaces;
using SZ.Core.Models;
using SZ.Core.Models.Db;

namespace SZ.Core
{
    public class ActiveZemstvoService : IActiveZemstvoService
    {
        public Guid? ActiveZemstvo { get; private set; }
        public int[] UserZemstva { get; private set; }


        public async Task Init(IUserSessionService userSessionService,
            IDbContextFactory<SZDb> dbContextFactory, IZemstvaManager zemstvaManager)
        {
            var dbProvider = new DBProvider(dbContextFactory);

            var zemstva = await zemstvaManager.GetUserZemstvaAsync(dbProvider, userSessionService);

            if (zemstva == null)
            {
                ActiveZemstvo = null;
                UserZemstva = null;
                return;
            }

            var countZemstva = await zemstva.CountAsync();

            if (countZemstva == 0)
            {
                ActiveZemstvo = null;
                UserZemstva = null;
                return;
            }

            if (countZemstva == 1)
                ActiveZemstvo = (await zemstva.FirstOrDefaultAsync()).Id;
            else
                ActiveZemstvo = null;

            UserZemstva = await zemstva.Select(x => x.ShowId).ToArrayAsync();
        }

        public void ChangeActive(Guid? zemstvoId)
        {
            ActiveZemstvo = zemstvoId;
        }
    }
}
