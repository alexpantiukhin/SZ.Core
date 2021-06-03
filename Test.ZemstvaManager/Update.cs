using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SZ.Core;

using TestData;

namespace Test.ZemstvaManager
{
    public class Update
    {
        TestSingletonEnvironment environment = new TestSingletonEnvironment();
        SZ.Core.ZemstvaManager _manager;
        TestDBFactory factory = new TestDBFactory();
        public Update()
        {
            _manager = new SZ.Core.ZemstvaManager(new UserManager(environment, null));
        }
    }
}
