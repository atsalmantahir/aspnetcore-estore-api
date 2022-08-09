namespace DomainLayer.Models
{
    public class UserProfile : EntityMeta
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
