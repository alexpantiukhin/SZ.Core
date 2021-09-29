using Al.Components.EF.Abstractions.Implementation;

using System.Threading.Tasks;

using SZ.Core;
using SZ.Core.Models;
using SZ.Core.Models.Db;

using TestData;

using Xunit;

namespace SZ.Test.Core
{
    public class GetCurrentUserAsync
    {
        readonly TestDBFactory factory = new ();
        readonly TestSingletonEnvironment singltoneEnv = new ();
        readonly TestScopeEnvironment scopeEnvironment = new ();
        /// <summary>
        /// ������ Identity �� ��������. ���������� null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task IdentityNullReturnNull()
        {
            //Arrange
            var dbProvider = new DBProvider<SZDb>(factory);
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
            var dbProvider = new DBProvider<SZDb>(factory);
            var user = await dbProvider.DB.AddUser(1);
            TestScopeEnvironment scopeEnvironment = new (user);
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
            TestScopeEnvironment scopeEnvironment = new ("User1", true);
            var dbProvider = new DBProvider<SZDb>(factory);
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
            var dbProvider = new DBProvider<SZDb>(factory);
            var user1 = await dbProvider.DB.AddUser(1);
            TestScopeEnvironment scopeEnvironment = new (user1, true);
            var userManager = new UserManager(singltoneEnv, null);

            //Act
            var result = await userManager.GetCurrentUserAsync(dbProvider, scopeEnvironment);

            //Assert
            Assert.True(user1.Id == result.Id, "���� ������������ ������, �� ������ ���� ���������");
        }
    }


}
