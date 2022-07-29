namespace DomainLayer.Models.ViewModels
{
    public class ProductsViewModel : AddProductModel
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
