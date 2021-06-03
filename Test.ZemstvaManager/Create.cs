using System;
using System.Threading.Tasks;

using SZ.Core;
using SZ.Core.Constants;
using SZ.Core.Models;

using TestData;

using Xunit;

namespace Test.ZemstvaManager
{
    public class Create
    {
        TestSingletonEnvironment environment = new TestSingletonEnvironment();
        SZ.Core.ZemstvaManager _manager;
        TestDBFactory factory = new TestDBFactory();

        public Create()
        {
            _manager = new SZ.Core.ZemstvaManager(new UserManager(environment, null));
        }

        [Fact]
        public async Task CurrentUserIsAdmin_ResultSuccessWithModel()
        {
            //arrange
            var scopeEnvironment = new TestScopeEnvironment(Settings.Users.AdminUserName, true);
            var zemstvoName = "�����";

            //act
            var result = await _manager.CreateAsync(new DBProvider(factory), scopeEnvironment, zemstvoName);

            //assert
            Assert.True(result.Success, "��� �������� ������� ������� ��������� ������ ���� ��������");
            Assert.True(result.Model != null, "��� �������� ������� ������� ������ ���� ���������� ������ �������");
            Assert.True(result.Model?.Name == zemstvoName, "��� �������� ������� ������� ������ ���� ���������� ������ ����������� �������");
        }

        [Fact]
        public async Task CurrentUserIsNotAdmin_ResultNotSuccess()
        {
            //arrange
            var scopeEnvironment = new TestScopeEnvironment(Settings.Users.AdminUserName, false);
            var zemstvoName = "�����";

            //act
            var instance = await _manager.CreateAsync(new DBProvider(factory), scopeEnvironment, zemstvoName);

            //assert
            Assert.True(!instance.Success, "��� ������� �������� ������� �� ������� �������� ������ ���� � �������");
            Assert.True(instance.Model == null, "��� �������� ������� �� ������� ������������ ������ ������ ���� null");
        }

        [Fact]
        public async Task CurrentUserIsAdminNullName_ResultNotSuccess()
        {
            //arrange
            var scopeEnvironment = new TestScopeEnvironment(Settings.Users.AdminUserName, true);
            string zemstvoName = null;

            //act
            var instance = await _manager.CreateAsync(new DBProvider(factory), scopeEnvironment, zemstvoName);

            //assert
            Assert.True(!instance.Success, "��� ������� �������� �������, �� ������ ��� ������ ���� ������");
        }
    }
}
