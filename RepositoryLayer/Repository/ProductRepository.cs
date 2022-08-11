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

        public async Task<bool> AddProduct(Product product)
        {
            try
            {
                context.Products.Add(product);
                await context.SaveChangesAsync();
                return true;
            }
            catch 
            {
                return false;
            }
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

        async Task<bool> IProductRepository.UpdateProduct(Product product)
        {
            try
            {
                context.Products.Update(product);
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
