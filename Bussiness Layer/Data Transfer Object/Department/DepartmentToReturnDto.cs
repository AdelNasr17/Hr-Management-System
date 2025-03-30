﻿

namespace Bussiness_Layer.Data_Transfer_Object.Department
{
    public class DepartmentToReturnDto
    {
        public int Id { get; set; } //PK
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
       
        public string? Description { get; set; }    
        public DateTime CreatedOn { get; set; }
    
    
    }
}
