using Al;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Text;
using System.Threading.Tasks;

using SZ.Core.Abstractions.Interfaces;
using SZ.Core.Constants;
using SZ.Core.Models.Db;

namespace SZ.Core
{
    public class UserManager : IUserManager
    {
        readonly ISZEnvironment _environment;
        readonly ILogger _logger;
        readonly IDBFactory _dBFactory;

        User CurrentUser = null;

        public UserManager(ISZEnvironment environment,
            ILoggerFactory loggerFactory,
            IDBFactory dBFactory)
        {
            _environment = environment;
            _logger = loggerFactory?.CreateLogger<UserManager>();
            _dBFactory = dBFactory;
        }


        public async Task<User> GetCurrentUserAsync(SZDb db = null)
        {
            if (CurrentUser != null)
                return CurrentUser;

            db = db ?? _dBFactory.DB;

            var identity = await _environment.GetCurrentUserIdentityAsync();

            if (identity?.IsAuthenticated != true)
                return null;

            CurrentUser = await db
                .Users
                .FirstOrDefaultAsync(x => x.UserName == identity.Name);

            return CurrentUser;
        }

        public async Task<bool> IsAdminAsync(Guid userId, SZDb db = null)
        {
            db = db ?? _dBFactory.DB;
            return await db.UserRoles
                .AnyAsync(x => x.UserId == userId
                && x.RoleId == Settings.Roles.AdminId);
        }

        public async Task<Result<string>> ChangePasswordAsync(Guid userId, SZDb db = null)
        {
            db = db ?? _dBFactory.DB;
            var result = new Result<string>(_logger);

            var currentUser = await GetCurrentUserAsync(db);

            if (currentUser == null)
                return result.AddError("Войдите в систему", $"Попытка смены пароля пользователю {userId} неавторизованным пользователем");

            var isAdmin = await IsAdminAsync(currentUser.Id, db);

            if (!isAdmin && currentUser.Id != userId)
                return result.AddError("Нет права смены пароля", $"Попытка смены пароля пользователю {userId} пользователем {currentUser.Id}, не имеющим на это прав");

            var dbUser = await db
                .Users.FindAsync(userId);

            if (dbUser == null)
                return result.AddError($"Пользователь {userId} не найден");

            var newPass = GeneratePassword();

            dbUser.PasswordHash = _environment.PasswordHasher.HashPassword(dbUser, newPass);

            try
            {
                var a = await db.SaveChangesAsync();

                if (a == 0)
                {
                    result.AddError("Ошибка создания пароля", "Пароль не сохранён в БД");
                    return result;
                }

                result.Model = newPass;

                return result;
            }
            catch (Exception e)
            {
                result.AddError(e, "Ошибка создания пароля");
                return result;
            }
        }


        private string GeneratePassword()
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
