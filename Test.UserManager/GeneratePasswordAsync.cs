using System;
using System.Linq;
using System.Threading.Tasks;

using SZ.Core;
using SZ.Core.Constants;
using SZ.Core.Models;

using TestData;

using Xunit;

namespace SZ.Test.Core
{
    /// <summary>
    /// ������ ������ ����� ������ ���������, ����� � ��� ������������
    /// </summary>
    public class GeneratePasswordAsync
    {
        TestSingletonEnvironment singltoneEnv = new TestSingletonEnvironment();
        TestDBFactory DBFactory;
        public GeneratePasswordAsync()
        {
            DBFactory = new TestDBFactory();
        }
        /// <summary>
        /// ������� ������������ �����. ��������� ����� ������
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CurrentUserAdmin_ReturnNewPass()
        {
            //Arrange
            var dbProvider = new DBProvider(DBFactory);
            var user1 = await dbProvider.DB.AddUser(1);
            string testPassHash = user1.PasswordHash;

            var environment = new TestScopeEnvironment(Settings.Users.AdminUserName, true);

            var userManager = new UserManager(singltoneEnv, null);

            //Act
            var result = await userManager.GeneratePasswordAsync(dbProvider, environment, user1.Id);

            //Assert
            Assert.False(testPassHash == dbProvider.DB.Users.FirstOrDefault(x => x.Id == user1.Id).PasswordHash,
                "����� ������ ����� ����� ����� ������");
        }

        /// <summary>
        /// ������� ������������ ��� ���� ������ ������. ��������� ����� ������
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CurrentUserSelfReturnNewPass()
        {
            //Arrange
            var dbProvider = new DBProvider(DBFactory);
            var user1 = await dbProvider.DB.AddUser(1);
            string testPassHash = user1.PasswordHash;
            var environment = new TestScopeEnvironment(user1.UserName, true);
            var userManager = new UserManager(singltoneEnv, null);

            //Act
            var result = await userManager.GeneratePasswordAsync(dbProvider, environment, user1.Id);

            //Assert
            Assert.NotEqual(testPassHash, dbProvider.DB.Users.FirstOrDefault(x => x.Id == user1.Id).PasswordHash);
        }

        /// <summary>
        /// ������� ������������ �� �����������. ���������� ������ � ������� � ������ ������
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CurrentUserNotAuthReturnErrorModel()
        {
            //Arrange
            var dbProvider = new DBProvider(DBFactory);
            var user1 = await dbProvider.DB.AddUser(1);
            string testPassHash = user1.PasswordHash;
            var environment = new TestScopeEnvironment(user1.UserName, false);

            var userManager = new UserManager(singltoneEnv, null);

            //Act
            var result = await userManager.GeneratePasswordAsync(dbProvider, environment, user1.Id);

            //Assert
            Assert.True(!string.IsNullOrWhiteSpace(result.UserMessage), "���������������� ������������ ��� ������� ����� ������ ������ �������� ������");
            Assert.True(testPassHash == dbProvider.DB.Users.FirstOrDefault(x => x.Id == user1.Id).PasswordHash,
                "��� ������� ����� ������ ���������������� ������������� ������ �������� ������ ������");
        }


        /// <summary>
        /// ������� �������� ������ ��������������� ������������. ���������� ������ � �������
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CurrentUserAdmin_ChangeUserNo_ReturnErrorModel()
        {
            //Arrange
            var dbProvider = new DBProvider(DBFactory);
            var environment = new TestScopeEnvironment(Settings.Users.AdminUserName, true);
            var userManager = new UserManager(singltoneEnv, null);

            //Act
            var result = await userManager.GeneratePasswordAsync(dbProvider, environment, Guid.NewGuid());

            //Assert
            Assert.True(!string.IsNullOrWhiteSpace(result.UserMessage), "������� �������� ������ ��������������� ������������ ������ ���������� ������ � �������");
        }

    }


}
