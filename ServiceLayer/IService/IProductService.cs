using DomainLayer.Models.ViewModels;

namespace ServiceLayer.IService
{
    public interface IProductService
    {
        void AddProduct(AddProductModel addProduct);
        Task<List<ProductsViewModel>> GetProducts();
        Task<ProductsViewModel> GetByIdAsync(Guid Id);
        Task<bool> DeleteAsync(Guid Id);
        void UpdateProduct(UpdateProductModel updateProduct);
    }
}
