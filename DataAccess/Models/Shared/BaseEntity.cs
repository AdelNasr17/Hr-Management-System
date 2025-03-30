

namespace DataAccess.Models.Shared
{
    public class BaseEntity
    {
        public int Id { get; set; } //PK
        public int CreatedBy { get; set; } = 1;//UserId
        public DateTime CreatedOn { get; set; }
        public int LastModifiedBy { get; set; } = 1; //UserId
        public DateTime? LastModifiedOn { get; set; }

        public bool IsDeleted { get; set; } // Soft Delete
    }
}
