using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Models.ViewModels;
using RepositoryLayer.IRepository;
using ServiceLayer.IService;

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

        public void AddProduct(AddProductModel addProduct)
        {
            var product = mapper.Map<Product>(addProduct);
            this.productRepository.AddProduct(product);
        }

        public async Task<List<ProductsViewModel>> GetProducts()
        {
            var products = await this.productRepository.GetAllAsync();
            return mapper.Map<List<ProductsViewModel>>(products);
        }

        public async Task<ProductsViewModel> GetByIdAsync(Guid Id)
        {
            var product = await this.productRepository.GetByIdAsync(Id);
            return mapper.Map<ProductsViewModel>(product);
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var product = await this.productRepository.GetByIdAsync(Id);

            var result = await this.productRepository.DeleteAsync(product);
            return result;
        }

        public void UpdateProduct(UpdateProductModel updateProduct)
        {
            var productToUpdate = mapper.Map<Product>(updateProduct);
            this.productRepository.UpdateProduct(productToUpdate);
        }
    }
}
