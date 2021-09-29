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
            var zemstvoName = "новое";
            TestScopeEnvironment scopeEnvironment = new (Settings.Users.AdminUserName, true);

            //act
            var result = await _manager.Creator.OperationAsync(new DBProvider<SZDb>(factory), scopeEnvironment, zemstvoName);

            //assert
            Assert.True(result.Success, "При создании админом земства результат должен быть успешным");
            Assert.True(result.Model != null, "При создании админом земства должна быть возвращена модель земства");
            Assert.True(result.Model?.Name == zemstvoName, "При создании админом земства должно быть возвращено именно создаваемое земство");
        }

        [Fact]
        public async Task CurrentUserIsNotAdmin_ResultNotSuccess()
        {
            //arrange
            TestScopeEnvironment scopeEnvironment = new (Settings.Users.AdminUserName, false);
            var zemstvoName = "новое";

            //act
            var result = await _manager.Creator.OperationAsync(new DBProvider<SZDb>(factory), scopeEnvironment, zemstvoName);

            //assert
            Assert.True(!result.Success, "При попытке создания земства не админом резульат должне быть с ошибкой");
            Assert.True(result.Model == null, "При создании земства не админом возвращаемая модель должна быть null");
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
            Assert.True(!result.Success, "При попытке создания земства, не указав имя должна быть ошибка");
        }
    }
}
