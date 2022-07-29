using DomainLayer.Models.ViewModels;

namespace ServiceLayer.IService
{
    public interface IProductService
    {
        void AddProduct(AddProductModel addProduct);
        List<ProductsViewModel> GetProducts();
    }
}
