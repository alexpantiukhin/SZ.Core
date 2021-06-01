using Microsoft.EntityFrameworkCore;

using System;
using System.Threading.Tasks;

using SZ.Core.Models.Db;

namespace SZ.Core.Abstractions.Interfaces
{
    public interface IActiveZemstvoService
    {
        Guid? ActiveZemstvo { get; }
        int[] UserZemstva { get; }

        Task Init(IUserSessionService userSessionService, IDbContextFactory<SZDb> dbContextFactory, IZemstvaManager zemstvaManager);
    }
}
