using System.Security.Principal;

namespace TestData
{
    public class TestIdentity : IIdentity
    {
        public string AuthenticationType { get; init; }
        public bool IsAuthenticated { get; init; }
        public string Name { get; init; }
    }
}
