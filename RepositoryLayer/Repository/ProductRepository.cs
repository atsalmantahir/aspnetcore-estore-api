using DomainLayer.Data;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
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

        async Task<List<Product>> IProductRepository.GetAllAsync()
        {
            return await context.Products.ToListAsync();
        }

        async Task<Product> IProductRepository.GetByIdAsync(Guid Id)
        {
            return await context.Products.FirstOrDefaultAsync(x => x.Id.Equals(Id));
        }

        async Task<bool> IProductRepository.DeleteAsync(Product product) 
        {
            try
            {
                context.Products.Remove(product);
                await context.SaveChangesAsync();
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
