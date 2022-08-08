namespace DomainLayer.Models
{
    public class OrderDetail : EntityMeta
    {
        public OrderDetail() 
        {
            this.Products = new HashSet<Product>();
        }
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public virtual Order Order { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
