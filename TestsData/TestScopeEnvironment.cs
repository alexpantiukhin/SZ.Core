using System.Security.Principal;
using System.Threading.Tasks;

using SZ.Core.Abstractions.Interfaces;
using SZ.Core.Models.Db;

namespace TestData
{
    public class TestScopeEnvironment : IUserSessionService
    {
        IIdentity Identity;

        public void SetTestUser(User user, bool isAuth = false)
        {
            SetTestUser(user.UserName, isAuth);
        }

        public void SetTestUser(string userName = null, bool isAuth = false)
        {
            if (string.IsNullOrWhiteSpace(userName))
                Identity = null;
            else
                Identity = new TestIdentity { Name = userName, IsAuthenticated = isAuth };
        }

        public Task<IIdentity> GetCurrentUserIdentityAsync()
        {
            return Task.FromResult(Identity);
        }
    }
}
