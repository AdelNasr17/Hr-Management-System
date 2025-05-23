﻿


namespace DataAccess.Models.Employee
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public string? ImageURL { get; set; }//ImageNeme
     

        public virtual Department.Department?  Department { get; set; }
        public int? DepartmentId { get; set; }
    }
}
