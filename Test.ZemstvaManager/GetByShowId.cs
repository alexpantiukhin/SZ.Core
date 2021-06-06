
using System.Threading.Tasks;

using SZ.Core;

using TestData;

using Xunit;

namespace Test.ZemstvaManager
{

    public class GetByShowId
    {
        TestSingletonEnvironment environment = new TestSingletonEnvironment();
        SZ.Core.ZemstvaManager _manager;
        TestDBFactory factory = new TestDBFactory();

        public GetByShowId()
        {
            var scopeEnvironment = new TestScopeEnvironment();
            _manager = new SZ.Core.ZemstvaManager(new UserManager(environment, null), scopeEnvironment, null);
        }
    }
}
