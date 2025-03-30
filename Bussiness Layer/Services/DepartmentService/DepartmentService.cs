
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccess.UnitOfWork;

namespace Bussiness_Layer.Services.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        //public DepartmentService(IDepartmentRepository departmentRepository,IMapper mapper) // 1.Injection
        //{
        //    _departmentRepository = departmentRepository;
        //    _mapper = mapper;
        //}

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper) // 1.Injection
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            //  CreateDepartmentDto --> Department 
            var department = _mapper.Map<Department>(departmentDto);
             _unitOfWork.departmentRepository.Add(department);

            return _unitOfWork.Complete();
        }

        public bool DeleteDepartment(int id)
        {
            var DepartmentRepo = _unitOfWork.departmentRepository;
            var department= DepartmentRepo.GetById(id);
            if(department != null)
                DepartmentRepo.Remove(department) ;               
            return  _unitOfWork.Complete()>0;
        }

        public IEnumerable<DepartmentToReturnDto> GetAllDepartments()
        {

            var departments = _unitOfWork.departmentRepository.GetAllQueryable().Where(D => D.IsDeleted == false)
                .ProjectTo<DepartmentToReturnDto>(_mapper.ConfigurationProvider);
            //    .Select(department => new DepartmentToReturnDto
            //{
            //    Id = department.Id,
            //    Name = department.Name,
            //    Description = department.Description,
            //    Code = department.Code,
            //    CreatedOn = department.CreatedOn,
            //}).AsNoTracking().ToList();

            return departments;
        }

        public DepartmentDetailsToReturnDto? GetDepartmentById(int id)
        {
            var department = _unitOfWork.departmentRepository.GetById(id);
            if(department is not null)
            {
                // Department ==> DepartmentDetailsToReturnDto
                var departments = _mapper.Map<DepartmentDetailsToReturnDto>(department);
                return departments;
               
            }
            return null;
        }

        public int UpdateDepartment(UpdateDepartmentDto departmentDto)
        {
            //UpdateDepartmentDto ==> Department
            var depapartment = _mapper.Map<Department>(departmentDto);

             _unitOfWork.departmentRepository.Update(depapartment);
            return _unitOfWork.Complete();


        }
    }
}
