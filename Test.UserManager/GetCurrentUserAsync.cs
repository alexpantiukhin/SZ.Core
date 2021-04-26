using System.Threading.Tasks;

using SZ.Core;
using SZ.Core.Models;

using TestData;

using Xunit;

namespace SZ.Test.Core
{
    public class GetCurrentUserAsync
    {
        TestDBFactory factory = new TestDBFactory();
        TestSingletonEnvironment singltoneEnv = new TestSingletonEnvironment();
        /// <summary>
        /// Объект Identity не определён. Возвращает null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task IdentityNullReturnNull()
        {
            //Arrange
            var dbProvider = new DBProvider(factory);
            var scopeEnvironment = new TestScopeEnvironment();
            var userManager = new UserManager(singltoneEnv, null);

            //Act
            var result = await userManager.GetCurrentUserAsync(dbProvider, scopeEnvironment);

            //Assert
            Assert.Null(result);
        }

        /// <summary>
        /// Пользователь не авторизован. Возвращает null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task IdentityIsNotAuthReturnNull()
        {
            //Arrange
            var dbProvider = new DBProvider(factory);
            var user = await dbProvider.DB.AddUser(1);
            var environment = new TestScopeEnvironment(user);
            var userManager = new UserManager(singltoneEnv, null);

            //Act
            var result = await userManager.GetCurrentUserAsync(dbProvider, environment);

            //Assert
            Assert.Null(result);
        }

        /// <summary>
        /// Имя пользователя не найдено. Возвращает null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task IdentityNotFoundReturnNull()
        {
            //Arrange
            var environment = new TestScopeEnvironment("User1", true);
            var dbProvider = new DBProvider(factory);
            var userManager = new UserManager(singltoneEnv, null);

            //Act
            var result = await userManager.GetCurrentUserAsync(dbProvider, environment);

            //Assert
            Assert.Null(result);
        }

        /// <summary>
        /// Пользователь найден. Возвращает текущего пользователя из базы
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task IdentityHasUserReturnUser()
        {
            //Arrange
            var dbProvider = new DBProvider(factory);
            var user1 = await dbProvider.DB.AddUser(1);
            var environment = new TestScopeEnvironment(user1, true);
            var userManager = new UserManager(singltoneEnv, null);

            //Act
            var result = await userManager.GetCurrentUserAsync(dbProvider, environment);

            //Assert
            Assert.True(user1.Id == result.Id, "Если пользователь найден, то должен быть возвращён");
        }
    }


}
