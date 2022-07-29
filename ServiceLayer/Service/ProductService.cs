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

        public List<ProductsViewModel> GetProducts()
        {
            var products = this.productRepository.GetAll();
            return mapper.Map<List<ProductsViewModel>>(products);
        }
    }
}
