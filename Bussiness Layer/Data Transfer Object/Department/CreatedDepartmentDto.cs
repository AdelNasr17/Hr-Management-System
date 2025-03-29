

namespace Bussiness_Layer.Data_Transfer_Object.Department
{
    public class CreatedDepartmentDto
    {
       
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
