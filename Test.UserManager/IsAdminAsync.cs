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
        /// ������������ �� ��������. ���������� false
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UserNotFoundReturnFalse()
        {
            //Arrange
            var environment = new TestScopeEnvironment();
            var db = new TestDBFactory();
            var userManager = new UserManager(singltoneEnv, environment, null, db);

            //Act
            var result = await userManager.IsAdminAsync(TestUsers.User1.Id);

            //Assert
            Assert.False(result);
        }

        /// <summary>
        /// ������������ �� �����. ���������� false
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UserNotAdminReturnFalse()
        {
            //Arrange
            var environment = new TestScopeEnvironment();
            var db = new TestDBFactory();
            var userManager = new UserManager(singltoneEnv, environment, null, db);
            await db.CreateDbContext().AddUser(1);

            //Act
            var result = await userManager.IsAdminAsync(TestUsers.User1.Id);

            //Assert
            Assert.False(result);
        }

        /// <summary>
        /// ������������ �����. ���������� true
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UserIsAdminReturnTrue()
        {
            //Arrange
            var environment = new TestScopeEnvironment();
            var db = new TestDBFactory();
            var userManager = new UserManager(singltoneEnv, environment, null, db);

            //Act
            var result = await userManager.IsAdminAsync(Settings.Users.AdminId);

            //Assert
            Assert.True(result);
        }

    }


}
