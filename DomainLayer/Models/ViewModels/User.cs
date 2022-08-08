namespace DomainLayer.Models.ViewModels
{
    public class User : EntityMeta
    {
        public Guid UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public virtual ApplicationUser IdentityUser { get; set; }
    }
}
