

namespace Bussiness_Layer.Services.DepartmentService
{
    public interface IDepartmentService
    {
        public int AddDepartment(CreatedDepartmentDto departmentDto);
        public IEnumerable<DepartmentToReturnDto> GetAllDepartments();
        public DepartmentDetailsToReturnDto? GetDepartmentById(int id);
        public bool DeleteDepartment(int id);
        public int UpdateDepartment(UpdateDepartmentDto departmentDto);
    }
}
