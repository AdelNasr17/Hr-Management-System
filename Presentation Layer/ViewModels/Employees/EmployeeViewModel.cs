using DataAccess.Models.Employee;
using DataAccess.Models.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Presentation_Layer.ViewModels.Employees
{
    public class EmployeeViewModel
    {
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }
        public string Gender { get; set; } = null!;
        public string EmployeeType { get; set; } = null!;     
        public DateTime CreatedOn { get; set; }     
        public DateTime? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; } // Soft Delete
        public int? DepartmentID { get; set; }
        public string? DepartmentName { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageURL { get; set; }



    }
}
