using System;
using System.Threading.Tasks;

using SZ.Core.Constants;

using TestData;

using Xunit;

namespace Test.ZemstvaManager
{
    public class Create
    {
        SZ.Core.ZemstvaManager _manager = new SZ.Core.ZemstvaManager();

        [Fact]
        public async Task CurrentUserIsAdmin_ReturnInstance()
        {
            //arrange
            var factory = new TestDBFactory();
            var db = factory.CreateDbContext();
            var scopeEnvironment = new TestScopeEnvironment(Settings.Users.AdminUserName, true);
            var zemstvoName = "�����";

            //act
            var result = await _manager.CreateAsync(scopeEnvironment, zemstvoName, db);

            //assert
            Assert.True(!result.Success, "��� �������� ������� ������� ��������� ������ ���� ��������");
            Assert.True(result.Model != null, "��� �������� ������� ������� ������ ���� ���������� ������ �������");
            Assert.True(result.Model?.Name == zemstvoName, "��� �������� ������� ������� ������ ���� ���������� ������ ����������� �������");
        }

        [Fact]
        public async Task CurrentUserIsNotAdmin_ReturnNull()
        {
            //arrange
            var factory = new TestDBFactory();
            var db = factory.CreateDbContext();
            var scopeEnvironment = new TestScopeEnvironment(Settings.Users.AdminUserName, true);
            var zemstvoName = "�����";

            //act
            var instance = await _manager.CreateAsync(scopeEnvironment, zemstvoName, db);

            //assert
            Assert.True(!instance.Success, "��� ������� �������� ������� �� ������� �������� ������ ���� � �������");
            Assert.True(instance.Model == null, "��� �������� ������� �� ������� ������������ ������ ������ ���� null");
        }
    }
}
