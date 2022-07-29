using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class SampleTest
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task HelloWorldTest() 
        {
            var application = new WebApplicationFactory<Program>();
            var httpClient = application.CreateClient(); 
            var response = await httpClient.GetAsync("api/users");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
