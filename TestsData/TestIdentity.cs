using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace TestData
{
    public class TestIdentity : IIdentity
    {
        public string AuthenticationType { get; init; }
        public bool IsAuthenticated { get; init; }
        public string Name { get; init; }
    }
}
