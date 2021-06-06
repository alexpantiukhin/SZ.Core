using Al;

using System.Threading.Tasks;

using SZ.Core;
using SZ.Core.Abstractions.Implementations;
using SZ.Core.Constants;
using SZ.Core.Models.Db;

using TestData;

using Xunit;

namespace Test.ZemstvaManager
{
    public class Create
    {
        TestSingletonEnvironment environment = new TestSingletonEnvironment();
        SZ.Core.ZemstvaManager _manager;
        TestDBFactory factory = new TestDBFactory();
        TestScopeEnvironment scopeEnvironment = new TestScopeEnvironment();

        public Create()
        {
            _manager = new SZ.Core.ZemstvaManager(new UserManager(environment, null), scopeEnvironment, null);
        }

        [Fact]
        public async Task CurrentUserIsAdmin_ResultSuccessWithModel()
        {
            //arrange
            var zemstvoName = "�����";
            scopeEnvironment.SetTestUser(Settings.Users.AdminUserName, true);

            //act
            var result = new Result<Zemstvo>();
            await _manager.Creator.OperationAsync(result, new DBProvider(factory), zemstvoName);

            //assert
            Assert.True(result.Success, "��� �������� ������� ������� ��������� ������ ���� ��������");
            Assert.True(result.Model != null, "��� �������� ������� ������� ������ ���� ���������� ������ �������");
            Assert.True(result.Model?.Name == zemstvoName, "��� �������� ������� ������� ������ ���� ���������� ������ ����������� �������");
        }

        [Fact]
        public async Task CurrentUserIsNotAdmin_ResultNotSuccess()
        {
            //arrange
            scopeEnvironment.SetTestUser(Settings.Users.AdminUserName, false);
            var zemstvoName = "�����";

            //act
            var result = new Result<Zemstvo>();
            await _manager.Creator.OperationAsync(result, new DBProvider(factory), zemstvoName);

            //assert
            Assert.True(!result.Success, "��� ������� �������� ������� �� ������� �������� ������ ���� � �������");
            Assert.True(result.Model == null, "��� �������� ������� �� ������� ������������ ������ ������ ���� null");
        }

        [Fact]
        public async Task CurrentUserIsAdminNullName_ResultNotSuccess()
        {
            //arrange
            scopeEnvironment.SetTestUser(Settings.Users.AdminUserName, true);
            string zemstvoName = null;

            //act
            var result = new Result<Zemstvo>();
            await _manager.Creator.OperationAsync(result, new DBProvider(factory), zemstvoName);

            //assert
            Assert.True(!result.Success, "��� ������� �������� �������, �� ������ ��� ������ ���� ������");
        }
    }
}
