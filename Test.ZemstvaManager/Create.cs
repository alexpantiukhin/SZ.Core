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
            var zemstvoName = "новое";

            //act
            var result = await _manager.CreateAsync(new DBProvider(factory), scopeEnvironment, zemstvoName);

            //assert
            Assert.True(result.Success, "При создании админом земства результат должне быть успешным");
            Assert.True(result.Model != null, "При создании админом земства должна быть возвращена модель земства");
            Assert.True(result.Model?.Name == zemstvoName, "При создании админом земства должно быть возвращено именно создаваемое земство");
        }

        [Fact]
        public async Task CurrentUserIsNotAdmin_ResultNotSuccess()
        {
            //arrange
            var scopeEnvironment = new TestScopeEnvironment(Settings.Users.AdminUserName, false);
            var zemstvoName = "новое";

            //act
            var instance = await _manager.CreateAsync(new DBProvider(factory), scopeEnvironment, zemstvoName);

            //assert
            Assert.True(!instance.Success, "При попытке создания земства не админом резульат должне быть с ошибкой");
            Assert.True(instance.Model == null, "При создании земства не админом возвращаемая модель должна быть null");
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
            Assert.True(!instance.Success, "При попытке создания земства, не указав имя должна быть ошибка");
        }
    }
}
