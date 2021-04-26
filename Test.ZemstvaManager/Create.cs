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
            var zemstvoName = "новое";

            //act
            var result = await _manager.CreateAsync(scopeEnvironment, zemstvoName, db);

            //assert
            Assert.True(!result.Success, "При создании админом земства результат должне быть успешным");
            Assert.True(result.Model != null, "При создании админом земства должна быть возвращена модель земства");
            Assert.True(result.Model?.Name == zemstvoName, "При создании админом земства должно быть возвращено именно создаваемое земство");
        }

        [Fact]
        public async Task CurrentUserIsNotAdmin_ReturnNull()
        {
            //arrange
            var factory = new TestDBFactory();
            var db = factory.CreateDbContext();
            var scopeEnvironment = new TestScopeEnvironment(Settings.Users.AdminUserName, true);
            var zemstvoName = "новое";

            //act
            var instance = await _manager.CreateAsync(scopeEnvironment, zemstvoName, db);

            //assert
            Assert.True(!instance.Success, "При попытке создания земства не админом резульат должне быть с ошибкой");
            Assert.True(instance.Model == null, "При создании земства не админом возвращаемая модель должна быть null");
        }
    }
}
