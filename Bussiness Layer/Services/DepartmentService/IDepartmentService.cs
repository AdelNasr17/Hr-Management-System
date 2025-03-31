

namespace Bussiness_Layer.Services.DepartmentService
{
    public interface IDepartmentService
    {
        public Task< int> AddDepartmentAsync(CreatedDepartmentDto departmentDto);
        public Task< IEnumerable<DepartmentToReturnDto>> GetAllDepartmentsAsync();
        public Task< DepartmentDetailsToReturnDto?> GetDepartmentByIdAsync(int id);
        public Task< bool> DeleteDepartmentAsync(int id);
        public Task< int> UpdateDepartmentAsync(UpdateDepartmentDto departmentDto);
    }
}
