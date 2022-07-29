using DomainLayer.Models.ViewModels;
using Microsoft.AspNetCore.Http;
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
        public IActionResult GetProducts()
        {
            var products = this.productService.GetProducts();
            return Ok(products);
        }
    }
}
