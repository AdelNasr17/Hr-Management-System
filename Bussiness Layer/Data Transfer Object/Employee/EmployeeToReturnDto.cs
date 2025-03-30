﻿

namespace Bussiness_Layer.Data_Transfer_Object.Employee
{
    public class EmployeeToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
     
        public string Gender { get; set; }=null!;
        [Display(Name="Employee Type")]
        public string EmployeeType { get; set; } = null!;
        public string? Department { get; set; }
        public string? ImageURL { get; set; }



    }
}
