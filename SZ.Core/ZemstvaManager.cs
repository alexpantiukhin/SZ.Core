using Al;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SZ.Core.Abstractions.Interfaces;
using SZ.Core.Models.Db;

namespace SZ.Core
{
    public class ZemstvaManager : IZemstvaManager
    {
        readonly UserManager _userManager;
        readonly IDbContextFactory<SZDb> _dbFactory;
        public ZemstvaManager(UserManager userManager, IDbContextFactory<SZDb> dbFactory)
        {
            _userManager = userManager;
            _dbFactory = dbFactory;
        }

        public Task<Result<Zemstvo>> CreateAsync(ISZScopeEnvironment scopeEnvironment, string name, SZDb db = null)
        {
            if
        }
    }
}
