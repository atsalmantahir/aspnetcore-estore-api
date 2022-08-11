using DomainLayer.Models;

namespace RepositoryLayer.IRepository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<bool> AddProduct(Product product);
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(Guid Id);
        Task<bool> DeleteAsync(Product product);
        Task<bool> UpdateProduct(Product product);
    }
}
