using System.Threading.Tasks;

using SZ.Core;
using SZ.Core.Constants;

using TestData;

using Xunit;

namespace SZ.Test.Core
{
    public class IsAdminAsync
    {
        TestSingletonEnvironment singltoneEnv = new TestSingletonEnvironment();
        /// <summary>
        /// Пользователь не определён. Возвращает false
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UserNotFoundReturnFalse()
        {
            //Arrange
            var factory = new TestDBFactory();
            var db = factory.CreateDbContext();
            var userManager = new UserManager(singltoneEnv, null, factory);

            //Act
            var result = await userManager.IsAdminAsync(TestUsers.User1.Id, db);

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
            var factory = new TestDBFactory();
            var db = factory.CreateDbContext();
            var userManager = new UserManager(singltoneEnv, null, factory);
            await db.AddUser(1);

            //Act
            var result = await userManager.IsAdminAsync(TestUsers.User1.Id, db);

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
            var factory = new TestDBFactory();
            var db = factory.CreateDbContext();
            var userManager = new UserManager(singltoneEnv, null, factory);

            //Act
            var result = await userManager.IsAdminAsync(Settings.Users.AdminId, db);

            //Assert
            Assert.True(result);
        }

    }


}
