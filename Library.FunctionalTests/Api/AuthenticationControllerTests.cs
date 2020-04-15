using Library.API;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.FunctionalTests.Api
{
    public class AuthenticationControllerTests : BaseControllerTests
    {
        public AuthenticationControllerTests(CustomWebApplicationFactory<Startup> factory) 
            : base(factory)
        {
        }

        [Fact]
        public async Task ReturnsAuthTokenTest()
        {
            var result = await GetJwtAsync();
            Assert.True(result is string);
        }
    }
}
