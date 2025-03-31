

namespace Bussiness_Layer.Services.EmployeeService
{
    public interface IEmployeeService
    {
        public Task< int> AddEmployeeAsync(CreatedEmployeeDto employeeDto);                 
        public Task< IEnumerable<EmployeeToReturnDto>> GetAllEmployeesAsync(string SearchValue);
        public Task< EmployeeDetailsToReturnDto?> GetEmployeeByIdAsync(int id);
        public Task< bool> DeleteEmployeeAsync(int id);
        public Task< int> UpdateEmployeeAsync(UpdateEmployeeDto EmployeeDto);
    }
}
