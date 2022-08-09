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
        public async Task<ObjectResult> GetProducts()
        {
            var response = await this.productService.GetProducts();
            return response;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ObjectResult> GetProduct(Guid id)
        {
            var response = await this.productService.GetByIdAsync(id);
            return response;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await this.productService.DeleteAsync(id);
            return Ok(product);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductModel updateProduct)
        {
            if (!id.Equals(updateProduct.Id)) 
            {
                return BadRequest("ID_MISMATCH");
            }

            this.productService.UpdateProduct(updateProduct);
            return Ok();
        }
    }
}
