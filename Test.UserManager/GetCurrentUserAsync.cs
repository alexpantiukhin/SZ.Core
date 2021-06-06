using System.Threading.Tasks;

using SZ.Core;
using SZ.Core.Abstractions.Implementations;
using SZ.Core.Models;

using TestData;

using Xunit;

namespace SZ.Test.Core
{
    public class GetCurrentUserAsync
    {
        TestDBFactory factory = new TestDBFactory();
        TestSingletonEnvironment singltoneEnv = new TestSingletonEnvironment();
        TestScopeEnvironment scopeEnvironment = new TestScopeEnvironment();
        /// <summary>
        /// ������ Identity �� ��������. ���������� null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task IdentityNullReturnNull()
        {
            //Arrange
            var dbProvider = new DBProvider(factory);
            var userManager = new UserManager(singltoneEnv, null);

            //Act
            var result = await userManager.GetCurrentUserAsync(dbProvider, scopeEnvironment);

            //Assert
            Assert.Null(result);
        }

        /// <summary>
        /// ������������ �� �����������. ���������� null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task IdentityIsNotAuthReturnNull()
        {
            //Arrange
            var dbProvider = new DBProvider(factory);
            var user = await dbProvider.DB.AddUser(1);
            scopeEnvironment.SetTestUser(user);
            var userManager = new UserManager(singltoneEnv, null);

            //Act
            var result = await userManager.GetCurrentUserAsync(dbProvider, scopeEnvironment);

            //Assert
            Assert.Null(result);
        }

        /// <summary>
        /// ��� ������������ �� �������. ���������� null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task IdentityNotFoundReturnNull()
        {
            //Arrange
            scopeEnvironment.SetTestUser("User1", true);
            var dbProvider = new DBProvider(factory);
            var userManager = new UserManager(singltoneEnv, null);

            //Act
            var result = await userManager.GetCurrentUserAsync(dbProvider, scopeEnvironment);

            //Assert
            Assert.Null(result);
        }

        /// <summary>
        /// ������������ ������. ���������� �������� ������������ �� ����
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task IdentityHasUserReturnUser()
        {
            //Arrange
            var dbProvider = new DBProvider(factory);
            var user1 = await dbProvider.DB.AddUser(1);
            scopeEnvironment.SetTestUser(user1, true);
            var userManager = new UserManager(singltoneEnv, null);

            //Act
            var result = await userManager.GetCurrentUserAsync(dbProvider, scopeEnvironment);

            //Assert
            Assert.True(user1.Id == result.Id, "���� ������������ ������, �� ������ ���� ���������");
        }
    }


}
