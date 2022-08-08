using DomainLayer.Models.Enums;

namespace DomainLayer.Models
{
    public class EntityMeta
    {
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set;}
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
