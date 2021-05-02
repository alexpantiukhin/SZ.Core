using System.Security.Principal;
using System.Threading.Tasks;

using SZ.Core.Abstractions.Interfaces;
using SZ.Core.Models.Db;

namespace TestData
{
    public class TestScopeEnvironment : IUserSessionService
    {
        IIdentity Identity;
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

        public async Task<IIdentity> GetCurrentUserIdentityAsync()
        {
            return Identity;
        }
    }
}
