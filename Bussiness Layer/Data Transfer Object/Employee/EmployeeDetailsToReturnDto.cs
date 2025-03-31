

namespace Bussiness_Layer.Data_Transfer_Object.Employee
{
    public class EmployeeDetailsToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }
        public string Gender { get; set; } = null!;
        public string EmployeeType { get; set; }= null!;
        public int CreatedBy { get; set; } //UserId
        public DateTime CreatedOn { get; set; }
        public int LastModifiedBy { get; set; } //UserId
        public DateTime? LastModifiedOn { get; set; }

        public bool IsDeleted { get; set; } // Soft Delete
        public string? DepartmentName { get; set; }
        public string? ImageURL { get; set; }





    }
}
