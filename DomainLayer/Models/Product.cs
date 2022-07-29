namespace DomainLayer.Models
{
    public class Product : EntityMeta
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Decimal UnitPrice { get; set; }
    }
}
