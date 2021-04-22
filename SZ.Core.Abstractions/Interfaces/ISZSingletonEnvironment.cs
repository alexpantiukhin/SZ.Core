using Microsoft.AspNetCore.Identity;

using SZ.Core.Models.Db;

namespace SZ.Core.Abstractions.Interfaces
{
    public interface ISZSingletonEnvironment
    {
        PasswordHasher<User> PasswordHasher { get; }
    }
}
