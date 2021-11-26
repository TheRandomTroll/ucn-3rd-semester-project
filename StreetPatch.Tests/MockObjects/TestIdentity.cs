using System.Security.Claims;

namespace StreetPatch.Tests.MockObjects
{
    public class TestIdentity : ClaimsIdentity
    {
        public TestIdentity(params Claim[] claims) : base(claims)
        {
        }
    }
}