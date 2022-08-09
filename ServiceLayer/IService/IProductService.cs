using DomainLayer.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ServiceLayer.IService
{
    public interface IProductService
    {
        Task<ObjectResult> AddProduct(AddProductModel addProduct);
        Task<ObjectResult> GetProducts();
        Task<ObjectResult> GetByIdAsync(Guid Id);
        Task<ObjectResult> DeleteAsync(Guid Id);
        Task<ObjectResult> UpdateProduct(UpdateProductModel updateProduct);
    }
}
