using Microsoft.AspNetCore.Identity;

using SZ.Core.Abstractions.Interfaces;
using SZ.Core.Models.Db;

namespace SZ.Core
{
    public class SingletonEnvironment : ISZSingletonEnvironment
    {
        public PasswordHasher<User> PasswordHasher { get; } = new PasswordHasher<User>();
    }
}
