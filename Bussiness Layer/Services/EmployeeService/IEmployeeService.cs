

namespace Bussiness_Layer.Services.EmployeeService
{
    public interface IEmployeeService
    {
        public int AddEmployee(CreatedEmployeeDto departmentDto);                 
        public IEnumerable<EmployeeToReturnDto> GetAllEmployees();
        public EmployeeDetailsToReturnDto? GetEmployeeById(int id);
        public bool DeleteEmployee(int id);
        public int UpdateEmployee(UpdateEmployeeDto EmployeeDto);
    }
}
