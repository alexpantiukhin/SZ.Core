
using System.Threading.Tasks;

using SZ.Core;

using TestData;

using Xunit;

namespace Test.ZemstvaManager
{

    public class GetByShowId
    {
        readonly TestSingletonEnvironment environment = new ();
        readonly SZ.Core.ZemstvaManager _manager;
        readonly TestDBFactory factory = new ();

        public GetByShowId()
        {
            var scopeEnvironment = new TestScopeEnvironment();
            _manager = new SZ.Core.ZemstvaManager(new UserManager(environment, null), null);
        }
    }
}
