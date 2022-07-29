using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.UsersTests
{
    public class CreateUserTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task CreateUser_ReturnOK() 
        {
            var application = new WebApplicationFactory<Program>();
            var httpClient = application.CreateClient();


        }
    }
}
