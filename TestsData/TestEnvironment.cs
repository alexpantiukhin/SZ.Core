using Microsoft.AspNetCore.Identity;

using System.Security.Principal;
using System.Threading.Tasks;

using SZ.Core.Abstractions.Interfaces;
using SZ.Core.Models.Db;

namespace TestData
{
    public class TestEnvironment : ISZEnvironment
    {
        IIdentity Identity;
        public TestEnvironment() : this(default(User), false) { }

        public TestEnvironment(User user, bool isAuth = false) : this(user.UserName, isAuth) { }

        public TestEnvironment(string userName = null, bool isAuth = false)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return;

            Identity = new TestIdentity { Name = userName, IsAuthenticated = isAuth };
        }

        public PasswordHasher<User> PasswordHasher { get; } = new PasswordHasher<User>();

        public async Task<IIdentity> GetCurrentUserIdentityAsync()
        {
            return Identity;
        }
    }
}
