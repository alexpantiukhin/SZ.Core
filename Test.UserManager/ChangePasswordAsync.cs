using System;
using System.Linq;
using System.Threading.Tasks;

using SZ.Core;
using SZ.Core.Constants;

using TestData;

using Xunit;

namespace SZ.Test.Core
{
    /// <summary>
    /// ������ ������ ����� ������ ��������� � ��� ������������
    /// </summary>
    public class ChangePasswordAsync
    {
        TestSingletonEnvironment singltoneEnv = new TestSingletonEnvironment();
        TestDBFactory DBFactory;
        public ChangePasswordAsync()
        {
            DBFactory = new TestDBFactory();
        }
        /// <summary>
        /// ������� ������������ �����. ��������� ����� ������
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CurrentUserAdminReturnNewPass()
        {
            //Arrange
            var user1 = DBFactory.DB.AddUser(1);
            string testPassHash = user1.PasswordHash;

            var environment = new TestScopeEnvironment(Settings.Users.AdminUserName, true);

            var userManager = new UserManager(singltoneEnv, environment, null, DBFactory);

            //Act
            var result = await userManager.ChangePasswordAsync(user1.Id);

            //Assert
            Assert.False(testPassHash == DBFactory.DB.Users.FirstOrDefault(x => x.Id == user1.Id).PasswordHash,
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
            var user1 = DBFactory.DB.AddUser(1);
            string testPassHash = user1.PasswordHash;
            var environment = new TestScopeEnvironment(user1.UserName, true);
            var userManager = new UserManager(singltoneEnv, environment, null, DBFactory);

            //Act
            var result = await userManager.ChangePasswordAsync(user1.Id);

            //Assert
            Assert.NotEqual(testPassHash, DBFactory.DB.Users.FirstOrDefault(x => x.Id == user1.Id).PasswordHash);
        }

        /// <summary>
        /// ������� ������������ �� �����������. ���������� ������ � ������� � ������ ������
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CurrentUserNotAuthReturnErrorModel()
        {
            //Arrange
            var user1 = DBFactory.DB.AddUser(1);
            string testPassHash = user1.PasswordHash;
            var environment = new TestScopeEnvironment(user1.UserName, false);

            var userManager = new UserManager(singltoneEnv, environment, null, DBFactory);

            //Act
            var result = await userManager.ChangePasswordAsync(user1.Id);

            //Assert
            Assert.True(!string.IsNullOrWhiteSpace(result.UserMessage), "���������������� ������������ ��� ������� ����� ������ ������ �������� ������");
            Assert.True(testPassHash == DBFactory.DB.Users.FirstOrDefault(x => x.Id == user1.Id).PasswordHash,
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
            var environment = new TestScopeEnvironment(Settings.Users.AdminUserName, true);
            var userManager = new UserManager(singltoneEnv, environment, null, DBFactory);

            //Act
            var result = await userManager.ChangePasswordAsync(Guid.NewGuid());

            //Assert
            Assert.True(!string.IsNullOrWhiteSpace(result.UserMessage), "������� �������� ������ ��������������� ������������ ������ ���������� ������ � �������");
        }

    }


}
