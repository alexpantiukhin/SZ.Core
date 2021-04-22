using Microsoft.AspNetCore.Identity;

using SZ.Core.Abstractions.Interfaces;
using SZ.Core.Models.Db;

namespace TestData
{
    public class TestSingletonEnvironment : ISZSingletonEnvironment
    {
        public PasswordHasher<User> PasswordHasher { get; } = new PasswordHasher<User>();
    }
}
