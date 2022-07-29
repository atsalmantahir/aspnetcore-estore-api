using DomainLayer.Data;
using DomainLayer.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.ProductsTests
{
    public class CreateProductTests
    {

        [Fact]
        public async Task CreateProduct_ReturnOK()
        {
            var application = new CustomWebApplicationFactory<Program>();
            var httpClient = application.CreateClient();

            var productToCreate = new AddProductModel 
            {
                Title = "Surf Excel",
                Description = "Description",
                UnitPrice = 100,  
            };

            var response = await httpClient.PostAsJsonAsync("api/products", productToCreate);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
