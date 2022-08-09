using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.IRepository;
using ServiceLayer.Handlers;
using ServiceLayer.IService;
using System.Net;

namespace ServiceLayer.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public Task<ObjectResult> AddProduct(AddProductModel addProduct)
        {
            var product = mapper.Map<Product>(addProduct);
            this.productRepository.AddProduct(product);
        }

        public async Task<ObjectResult> GetProducts()
        {
            var products = await this.productRepository.GetAllAsync();
            var response = new OkObjectResult(mapper.Map<List<ProductsViewModel>>(products));
            return response;
        }

        public async Task<ObjectResult> GetByIdAsync(Guid Id)
        {
            var product = await this.productRepository.GetByIdAsync(Id);
            var response = new ObjectResult(mapper.Map<ProductsViewModel>(product));
            return response;
        }

        public async Task<ObjectResult> DeleteAsync(Guid Id)
        {
            var product = await this.productRepository.GetByIdAsync(Id);
            if (product == null) 
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound,
                    "PRODUCT_NOT_FOUND");
            }

            var result = await this.productRepository.DeleteAsync(product);
            return result ? new ObjectResult("PRODUCT_HAS_BEEN_DELETED") : new ObjectResult("PRODUCT_HAS_NOT_BEEN_DELETED");
        }

        public Task<ObjectResult> UpdateProduct(UpdateProductModel updateProduct)
        {
            var productToUpdate = mapper.Map<Product>(updateProduct);
            this.productRepository.UpdateProduct(productToUpdate);
        }
    }
}
