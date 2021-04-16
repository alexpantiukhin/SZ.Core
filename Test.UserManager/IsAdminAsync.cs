using System.Threading.Tasks;

using SZ.Core;
using SZ.Core.Constants;

using TestData;

using Xunit;

namespace SZ.Test.Core
{
    public class IsAdminAsync
    {
        /// <summary>
        /// Пользователь не определён. Возвращает false
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UserNotFoundReturnFalse()
        {
            //Arrange
            var environment = new TestEnvironment();
            var db = new TestDBFactory();
            var userManager = new UserManager(environment, null, db);

            //Act
            var result = await userManager.IsAdminAsync(TestUsers.User1.Id);

            //Assert
            Assert.False(result);
        }

        /// <summary>
        /// Пользователь не админ. Возвращает false
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UserNotAdminReturnFalse()
        {
            //Arrange
            var environment = new TestEnvironment();
            var db = new TestDBFactory();
            var userManager = new UserManager(environment, null, db);
            db.DB.AddUser(1);

            //Act
            var result = await userManager.IsAdminAsync(TestUsers.User1.Id);

            //Assert
            Assert.False(result);
        }

        /// <summary>
        /// Пользователь админ. Возвращает true
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UserIsAdminReturnTrue()
        {
            //Arrange
            var environment = new TestEnvironment();
            var db = new TestDBFactory();
            var userManager = new UserManager(environment, null, db);

            //Act
            var result = await userManager.IsAdminAsync(Settings.Users.AdminId);

            //Assert
            Assert.True(result);
        }

    }


}
