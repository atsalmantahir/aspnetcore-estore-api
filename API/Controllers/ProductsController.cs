using DomainLayer.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.IService;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpPost]
        public IActionResult AddProduct(AddProductModel addProduct) 
        {
            this.productService.AddProduct(addProduct);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await this.productService.GetProducts();
            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await this.productService.GetByIdAsync(id);
            return Ok(product);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await this.productService.DeleteAsync(id);
            return Ok(product);
        }
    }
}
