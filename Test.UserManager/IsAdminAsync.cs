using System.Threading.Tasks;

using SZ.Core;
using SZ.Core.Abstractions.Implementations;
using SZ.Core.Constants;
using SZ.Core.Models;

using TestData;

using Xunit;

namespace SZ.Test.Core
{
    public class IsAdminAsync
    {
        TestDBFactory factory = new TestDBFactory();
        TestSingletonEnvironment singltoneEnv = new TestSingletonEnvironment();
        /// <summary>
        /// Пользователь не определён. Возвращает false
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UserNotFoundReturnFalse()
        {
            //Arrange
            var dbProvider = new DBProvider(factory);
            var db = factory.CreateDbContext();
            var userManager = new UserManager(singltoneEnv, null);

            //Act
            var result = await userManager.IsAdminAsync(dbProvider, TestUsers.User1.Id);

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
            var dbProvider = new DBProvider(factory);
            var userManager = new UserManager(singltoneEnv, null);
            await dbProvider.DB.AddUser(1);


            //Act
            var result = await userManager.IsAdminAsync(dbProvider, TestUsers.User1.Id);

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
            var dbProvider = new DBProvider(factory);
            var userManager = new UserManager(singltoneEnv, null);

            //Act
            var result = await userManager.IsAdminAsync(dbProvider, Settings.Users.AdminId);

            //Assert
            Assert.True(result);
        }

    }


}
