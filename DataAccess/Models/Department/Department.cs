
namespace DataAccess.Models.Department
{
    public class Department :BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Employee.Employee> Employees { get; set; } = new List<Employee.Employee>();
    }
}
