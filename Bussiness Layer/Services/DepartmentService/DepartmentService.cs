
namespace Bussiness_Layer.Services.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository) // 1.Injection
        {
            _departmentRepository = departmentRepository;
        }

        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            var depapartment = new Department
            {
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreatedBy = 1,
                CreatedOn = departmentDto.CreationDate,
                LastModifiedBy = 1,
                LastModifiedOn=DateTime.UtcNow,

            };
            return _departmentRepository.Add(depapartment);



        }

        public bool DeleteDepartment(int id)
        {
            var department= _departmentRepository.GetById(id);
            if(department != null)          
                return _departmentRepository.Remove(department) > 0;
                            
            return false;
        }

        public IEnumerable<DepartmentToReturnDto> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAllQueryable().Where(D => D.IsDeleted == false).Select(department => new DepartmentToReturnDto
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description,
                Code = department.Code,
                CreatedOn = DateOnly.FromDateTime(department.CreatedOn),
            }).AsNoTracking().ToList();

            return departments;
        }

        public DepartmentDetailsToReturnDto? GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetById(id);
            if(department is not null)
            {

                return new DepartmentDetailsToReturnDto
                {
                    Id = department.Id,
                    Name = department.Name,
                    Description = department.Description,
                    Code = department.Code,
                    CreatedBy = department.CreatedBy,
                    CreatedOn = department.CreatedOn,
                    LastModifiedBy = department.LastModifiedBy,
                    LastModifiedOn = department.LastModifiedOn,
                    IsDeleted = department.IsDeleted,
                    
                };
            }
            return null;
        }

        public int UpdateDepartment(UpdateDepartmentDto departmentDto)
        {
            var depapartment = new Department
            {
                Id = departmentDto.Id,
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreatedBy = 1,
                CreatedOn = departmentDto.CreationDate,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,

            };
            return _departmentRepository.Update(depapartment);



        }
    }
}
