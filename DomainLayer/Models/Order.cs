namespace DomainLayer.Models
{
    public class Order : EntityMeta
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public virtual ApplicationUser Customer { get; set; }
    }
}
