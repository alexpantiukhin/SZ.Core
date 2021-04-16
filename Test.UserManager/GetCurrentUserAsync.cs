using System.Threading.Tasks;

using SZ.Core;

using TestData;

using Xunit;

namespace SZ.Test.Core
{
    public class GetCurrentUserAsync
    {
        /// <summary>
        /// Объект Identity не определён. Возвращает null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task IdentityNullReturnNull()
        {
            //Arrange
            var environment = new TestEnvironment();
            var db = new TestDBFactory();
            var userManager = new UserManager(environment, null, db);

            //Act
            var result = await userManager.GetCurrentUserAsync();

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
            var db = new TestDBFactory();
            var user = db.DB.AddUser(1);
            var environment = new TestEnvironment(user);
            var userManager = new UserManager(environment, null, db);

            //Act
            var result = await userManager.GetCurrentUserAsync();

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
            var environment = new TestEnvironment("User1", true);
            var db = new TestDBFactory();
            var userManager = new UserManager(environment, null, db);

            //Act
            var result = await userManager.GetCurrentUserAsync();

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
            var db = new TestDBFactory();
            var user1 = db.DB.AddUser(1);
            var environment = new TestEnvironment(user1, true);
            var userManager = new UserManager(environment, null, db);

            //Act
            var result = await userManager.GetCurrentUserAsync();

            //Assert
            Assert.True(user1.Id == result.Id, "Если пользователь найден, то должен быть возвращён");
        }
    }


}
