using Al;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Text;
using System.Threading.Tasks;

using SZ.Core.Abstractions.Interfaces;
using SZ.Core.Constants;
using SZ.Core.Models;
using SZ.Core.Models.Db;

namespace SZ.Core
{
    public class UserManager : IUserManager
    {
        readonly ISZSingletonEnvironment _environment;
        readonly ILogger _logger;

        public UserManager(ISZSingletonEnvironment environment,
            ILoggerFactory loggerFactory)
        {
            _environment = environment;
            _logger = loggerFactory?.CreateLogger<UserManager>();
        }


        public async Task<User> GetCurrentUserAsync(IDBProvider<SZDb> provider, IUserSessionService userSessionEnvironment)
        {
            var identity = await userSessionEnvironment.GetCurrentUserIdentityAsync();

            if (identity?.IsAuthenticated != true)
                return null;

            var currentUser = await provider.DB
                .Users
                .FirstOrDefaultAsync(x => x.UserName == identity.Name);

            return currentUser;
        }

        public async Task<bool> IsAdminAsync(IDBProvider<SZDb> provider, Guid userId)
        {
            return await provider.DB.UserRoles
                .AnyAsync(x => x.UserId == userId
                && x.RoleId == Settings.Roles.AdminId);
        }

        public async Task<Result<string>> GeneratePasswordAsync(IDBProvider<SZDb> provider, IUserSessionService userSessionEnvironment, Guid userId)
        {
            var result = new Result<string>(_logger);

            var currentUser = await GetCurrentUserAsync(provider, userSessionEnvironment);

            if (currentUser == null)
                return result.AddError("Войдите в систему", $"Попытка смены пароля пользователю {userId} неавторизованным пользователем");

            var isAdmin = await IsAdminAsync(provider, currentUser.Id);

            if (!isAdmin && currentUser.Id != userId)
                return result.AddError("Нет права смены пароля", $"Попытка смены пароля пользователю {userId} пользователем {currentUser.Id}, не имеющим на это прав");

            var dbUser = await provider.DB
                .Users.FindAsync(userId);

            if (dbUser == null)
                return result.AddError($"Пользователь {userId} не найден");

            var newPass = GeneratePassword();

            dbUser.PasswordHash = _environment.PasswordHasher.HashPassword(dbUser, newPass);

            try
            {
                var a = await provider.DB.SaveChangesAsync();

                if (a == 0)
                {
                    result.AddError("Ошибка создания пароля", "Пароль не сохранён в БД");
                    return result;
                }

                result.AddModel(newPass);

                return result;
            }
            catch (Exception e)
            {
                result.AddError(e, "Ошибка создания пароля");
                return result;
            }
        }


        string GeneratePassword()
        {
            int length = Settings.PasswordOptions.RequiredLength;
            bool nonAlphanumeric = Settings.PasswordOptions.RequireNonAlphanumeric;
            bool digit = Settings.PasswordOptions.RequireDigit;
            bool lowercase = Settings.PasswordOptions.RequireLowercase;
            bool uppercase = Settings.PasswordOptions.RequireUppercase;

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (password.Length < length)
            {
                char c = (char)random.Next(32, 126);

                password.Append(c);

                if (char.IsDigit(c))
                    digit = false;
                else if (char.IsLower(c))
                    lowercase = false;
                else if (char.IsUpper(c))
                    uppercase = false;
                else if (!char.IsLetterOrDigit(c))
                    nonAlphanumeric = false;
            }

            if (nonAlphanumeric)
                password.Append((char)random.Next(33, 48));
            if (digit)
                password.Append((char)random.Next(48, 58));
            if (lowercase)
                password.Append((char)random.Next(97, 123));
            if (uppercase)
                password.Append((char)random.Next(65, 91));

            return password.ToString();
        }

    }
}
