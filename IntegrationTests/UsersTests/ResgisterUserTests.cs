using DomainLayer.Models.ViewModels;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.UsersTests
{
    public class ResgisterUserTests
    {
        private readonly string baseUrl = "api/users/register";

        [Fact]
        public async Task RegisterUser_ReturnOK()
        {
            var application = new CustomWebApplicationFactory<Program>();
            var httpClient = application.CreateClient();

            var userToRegister = new RegisterModel 
            {
                Email = "test_user@gmail.com",
                Username = "test_user",
                Password = "password123",
            };

            var response = await httpClient.PostAsJsonAsync(baseUrl, userToRegister);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task WhenPasswordIsNotBeingProvidedInRegisterUser_ReturnBadRequest()
        {
            var application = new CustomWebApplicationFactory<Program>();
            var httpClient = application.CreateClient();

            var userToRegister = new RegisterModel
            {
                Email = "test_user@gmail.com",
                Username = "test_user",
            };

            var response = await httpClient.PostAsJsonAsync(baseUrl, userToRegister);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task WhenEmailIsNotBeingProvidedInRegisterUser_ReturnBadRequest()
        {
            var application = new CustomWebApplicationFactory<Program>();
            var httpClient = application.CreateClient();

            var userToRegister = new RegisterModel
            {
                Password = "password123",
                Username = "test_user",
            };

            var response = await httpClient.PostAsJsonAsync(baseUrl, userToRegister);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task WhenUsernameIsNotBeingProvidedInRegisterUser_ReturnBadRequest()
        {
            var application = new CustomWebApplicationFactory<Program>();
            var httpClient = application.CreateClient();

            var userToRegister = new RegisterModel
            {
                Email = "test_user@gmail.com",
                Username = "test_user",
            };

            var response = await httpClient.PostAsJsonAsync(baseUrl, userToRegister);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
