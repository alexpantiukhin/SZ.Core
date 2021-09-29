using Al.Components.Identity.Abstractions.Interfaces;

using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

using SZ.Core.Models.Db;

namespace TestData
{
    public class TestScopeEnvironment : IUserSessionService
    {
        readonly IIdentity Identity;
        /// <summary>
        /// Пользователь не зашёл
        /// </summary>
        public TestScopeEnvironment() : this(default(string), false) { }

        public TestScopeEnvironment(User user, bool isAuth = false) : this(user.UserName, isAuth) { }

        public TestScopeEnvironment(string userName = null, bool isAuth = false)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return;

            Identity = new TestIdentity { Name = userName, IsAuthenticated = isAuth };
        }

        public Task<IIdentity> GetCurrentUserIdentityAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Identity);
        }
    }
}
