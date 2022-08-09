using DomainLayer.Models.ViewModels;
using IntegrationTests.Helpers;
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

        [Fact]
        public async Task WhenUserIsCreated_ReturnCreated()
        {
            var application = new CustomWebApplicationFactory<Program>();
            var httpClient = application.CreateClient();

            var userToRegister = new RegisterModel
            {
                Email = "test_user@gmail.com",
                Username = "test_user",
                Password = "Password1!",
                UserRole = DomainLayer.Models.Enums.UserRole.ADMIN,
            };

            var response = await httpClient.PostAsJsonAsync(baseUrl, userToRegister);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task WhenDuplicateUserEmailIsProvided_ReturnConflict()
        {
            var application = new CustomWebApplicationFactory<Program>();
            var httpClient = application.CreateClient();

            var userToRegister = new RegisterModel
            {
                Email = "test_user@gmail.com",
                Username = "test_user",
                Password = "Password1!",
                UserRole = DomainLayer.Models.Enums.UserRole.ADMIN,
            };

            var response = await httpClient.PostAsJsonAsync(baseUrl, userToRegister);


            var duplicateUserToRegister = new RegisterModel
            {
                Email = "test_user@gmail.com",
                Username = "test_user_New",
                Password = "Password1!",
                UserRole = DomainLayer.Models.Enums.UserRole.ADMIN,
            };

            var duplicateResponse = await httpClient.PostAsJsonAsync(baseUrl, duplicateUserToRegister);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(HttpStatusCode.Conflict, duplicateResponse.StatusCode);
        }

        [Fact]
        public async Task WhenDuplicateUsernameIsProvided_ReturnConflict()
        {
            var application = new CustomWebApplicationFactory<Program>();
            var httpClient = application.CreateClient();

            var userToRegister = new RegisterModel
            {
                Email = $"test_user{TestHelper.RandomString()}@gmail.com",
                Username = $"test_user{TestHelper.RandomString()}",
                Password = "Password1!",
                UserRole = DomainLayer.Models.Enums.UserRole.ADMIN,
            };

            var response = await httpClient.PostAsJsonAsync(baseUrl, userToRegister);


            var duplicateUserToRegister = new RegisterModel
            {
                Email = "test_user_new@gmail.com",
                Username = $"test_user{TestHelper.RandomString()}",
                Password = "Password1!",
                UserRole = DomainLayer.Models.Enums.UserRole.ADMIN,
            };

            var duplicateResponse = await httpClient.PostAsJsonAsync(baseUrl, duplicateUserToRegister);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(HttpStatusCode.Conflict, duplicateResponse.StatusCode);
        }

        [Fact]
        public async Task WhenDuplicateUserEmailAndUsernameIsProvided_ReturnConflict()
        {
            var application = new CustomWebApplicationFactory<Program>();
            var httpClient = application.CreateClient();

            var userToRegister = new RegisterModel
            {
                Email = "test_user@gmail.com",
                Username = "test_user",
                Password = "Password1!",
                UserRole = DomainLayer.Models.Enums.UserRole.ADMIN,
            };

            var response = await httpClient.PostAsJsonAsync(baseUrl, userToRegister);


            var duplicateUserToRegister = new RegisterModel
            {
                Email = "test_user@gmail.com",
                Username = "test_user",
                Password = "Password1!",
                UserRole = DomainLayer.Models.Enums.UserRole.ADMIN,
            };

            var duplicateResponse = await httpClient.PostAsJsonAsync(baseUrl, duplicateUserToRegister);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(HttpStatusCode.Conflict, duplicateResponse.StatusCode);
        }
    }
}
