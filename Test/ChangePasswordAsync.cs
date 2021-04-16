using System.Linq;
using System.Threading.Tasks;

using SZ.Core;
using SZ.Core.Constants;

using TestData;

using Xunit;

namespace SZ.Test.Core
{
    /// <summary>
    /// Менять пароль могут только секретари и сам пользователь
    /// </summary>
    public class ChangePasswordAsync
    {

        /// <summary>
        /// Текущий пользователь админ. Возвращён новый пароль
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CurrentUserAdminReturnNewPass()
        {
            //Arrange
            var dbFactory = new TestDBFactory();
            var user1 = dbFactory.DB.AddUser(1);
            string testPassHash = user1.PasswordHash;

            var environment = new TestEnvironment(new TestIdentity
            {
                Name = Settings.Users.AdminUserName,
                IsAuthenticated = true
            });

            var userManager = new UserManager(environment, null, dbFactory);

            //Act
            var result = await userManager.ChangePasswordAsync(user1.Id);

            //Assert
            Assert.False(testPassHash == dbFactory.DB.Users.FirstOrDefault(x => x.Id == user1.Id).PasswordHash,
                "Админ должен иметь право смены пароля");
        }

        /// <summary>
        /// Текущий пользователь сам себе меняет пароль. Возвращён новый пароль
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CurrentUserSelfReturnNewPass()
        {
            //Arrange
            var dbFactory = new TestDBFactory();
            var user1 = dbFactory.DB.AddUser(1);
            string testPassHash = user1.PasswordHash;
            var environment = new TestEnvironment(new TestIdentity { Name = user1.UserName, IsAuthenticated = true });
            var userManager = new UserManager(environment, null, dbFactory);

            //Act
            var result = await userManager.ChangePasswordAsync(user1.Id);

            //Assert
            Assert.NotEqual(testPassHash, dbFactory.DB.Users.FirstOrDefault(x => x.Id == user1.Id).PasswordHash);
        }

        /// <summary>
        /// Текущий пользователь не авторизован. Возвращена модель с ошибкой и пароль старый
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CurrentUserNotAuthReturnErrorModel()
        {
            //Arrange
            var dbFactory = new TestDBFactory();
            var user1 = dbFactory.DB.AddUser(1);
            string testPassHash = user1.PasswordHash;
            var environment = new TestEnvironment(new TestIdentity { Name = user1.UserName, IsAuthenticated = false });

            var userManager = new UserManager(environment, null, dbFactory);

            //Act
            var result = await userManager.ChangePasswordAsync(user1.Id);

            //Assert
            Assert.True(!string.IsNullOrWhiteSpace(result.UserMessage), "Неавторизованный пользователь при попытке смены пароля должен получать ошибку");
            Assert.True(testPassHash == dbFactory.DB.Users.FirstOrDefault(x => x.Id == user1.Id).PasswordHash,
                "При попытке смены пароля неавторизованным пользователем должен остаться старый пароль");
        }
    }


}
