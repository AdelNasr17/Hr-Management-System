

namespace Bussiness_Layer.Data_Transfer_Object.Department
{
    public class DepartmentDetailsToReturnDto
    {
        public int Id { get; set; } //PK
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public int CreatedBy { get; set; } = 1; //UserId
        public DateTime CreatedOn { get; set; }
        public int LastModifiedBy { get; set; } = 1;//UserId
        public DateTime? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; } // Soft Delete
 
    }
}
