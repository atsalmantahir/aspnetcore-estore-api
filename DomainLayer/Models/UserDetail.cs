namespace DomainLayer.Models
{
    public class UserDetail : EntityMeta
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
