using Al.Components.EF.Abstractions.Implementation;

using System.Threading.Tasks;

using SZ.Core;
using SZ.Core.Constants;
using SZ.Core.Models.Db;

using TestData;

using Xunit;

namespace Test.ZemstvaManager
{
    public class Create
    {
        readonly TestSingletonEnvironment environment = new ();
        readonly SZ.Core.ZemstvaManager _manager;
        readonly TestDBFactory factory = new ();

        public Create()
        {
            _manager = new SZ.Core.ZemstvaManager(new UserManager(environment, null), null);
        }

        [Fact]
        public async Task CurrentUserIsAdmin_ResultSuccessWithModel()
        {
            //arrange
            var zemstvoName = "�����";
            TestScopeEnvironment scopeEnvironment = new (Settings.Users.AdminUserName, true);

            //act
            var result = await _manager.Creator.OperationAsync(new DBProvider<SZDb>(factory), scopeEnvironment, zemstvoName);

            //assert
            Assert.True(result.Success, "��� �������� ������� ������� ��������� ������ ���� ��������");
            Assert.True(result.Model != null, "��� �������� ������� ������� ������ ���� ���������� ������ �������");
            Assert.True(result.Model?.Name == zemstvoName, "��� �������� ������� ������� ������ ���� ���������� ������ ����������� �������");
        }

        [Fact]
        public async Task CurrentUserIsNotAdmin_ResultNotSuccess()
        {
            //arrange
            TestScopeEnvironment scopeEnvironment = new (Settings.Users.AdminUserName, false);
            var zemstvoName = "�����";

            //act
            var result = await _manager.Creator.OperationAsync(new DBProvider<SZDb>(factory), scopeEnvironment, zemstvoName);

            //assert
            Assert.True(!result.Success, "��� ������� �������� ������� �� ������� �������� ������ ���� � �������");
            Assert.True(result.Model == null, "��� �������� ������� �� ������� ������������ ������ ������ ���� null");
        }

        [Fact]
        public async Task CurrentUserIsAdminNullName_ResultNotSuccess()
        {
            //arrange
            TestScopeEnvironment scopeEnvironment = new (Settings.Users.AdminUserName, true);
            string zemstvoName = null;

            //act
            var result = await _manager.Creator.OperationAsync(new DBProvider<SZDb>(factory), scopeEnvironment, zemstvoName);

            //assert
            Assert.True(!result.Success, "��� ������� �������� �������, �� ������ ��� ������ ���� ������");
        }
    }
}
