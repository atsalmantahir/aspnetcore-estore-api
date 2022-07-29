using DomainLayer.Data;
using DomainLayer.Models;
using RepositoryLayer.IRepository;

namespace RepositoryLayer.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(StoreDbContext context) : base(context)
        {
        }

        public void AddProduct(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        List<Product> IProductRepository.GetAll()
        {
            return context.Products.ToList();
        }
    }
}
