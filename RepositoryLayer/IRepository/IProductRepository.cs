using DomainLayer.Models;

namespace RepositoryLayer.IRepository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        void AddProduct(Product product);
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(Guid Id);
        Task<bool> DeleteAsync(Product product);
        void UpdateProduct(Product product);
    }
}
