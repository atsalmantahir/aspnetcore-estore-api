using DomainLayer.Models;

namespace RepositoryLayer.IRepository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        void AddProduct(Product product);

        List<Product> GetAll();
    }
}
