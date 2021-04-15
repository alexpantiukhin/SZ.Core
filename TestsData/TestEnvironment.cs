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
        public TestEnvironment(TestIdentity identity)
        {
            Identity = identity;
        }

        public PasswordHasher<User> PasswordHasher { get; } = new PasswordHasher<User>();

        public async Task<IIdentity> GetCurrentUserIdentityAsync()
        {
            return Identity;
        }
    }
}
