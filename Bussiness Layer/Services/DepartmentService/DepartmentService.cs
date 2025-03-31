
namespace Bussiness_Layer.Services.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        #region Services
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        ///public DepartmentService(IDepartmentRepository departmentRepository,IMapper mapper) // 1.Injection
        ///{
        ///    _departmentRepository = departmentRepository;
        ///    _mapper = mapper;
        ///}

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper) // 1.Injection
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #endregion



        #region Method
        public async Task<int> AddDepartmentAsync(CreatedDepartmentDto departmentDto)
        {
            //  CreateDepartmentDto --> Department 
            var department = _mapper.Map<Department>(departmentDto);
            _unitOfWork.departmentRepository.Add(department);

            return await _unitOfWork.CompleteAsync().ConfigureAwait(false);
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var DepartmentRepo = _unitOfWork.departmentRepository;
            var department = await DepartmentRepo.GetByIdAsync(id).ConfigureAwait(false);
            if (department != null)
                DepartmentRepo.Remove(department);
            return await _unitOfWork.CompleteAsync().ConfigureAwait(false) > 0;
        }

        public async Task<IEnumerable<DepartmentToReturnDto>> GetAllDepartmentsAsync()
        {

            var departments = await _unitOfWork.departmentRepository.GetAllQueryable().Where(D => D.IsDeleted == false)
                .ProjectTo<DepartmentToReturnDto>(_mapper.ConfigurationProvider).AsNoTracking().ToListAsync().ConfigureAwait(false);

            return departments;
        }

        public async Task<DepartmentDetailsToReturnDto?> GetDepartmentByIdAsync(int id)
        {
            var department = await _unitOfWork.departmentRepository.GetByIdAsync(id).ConfigureAwait(false);
            if (department is not null)
            {
                // Department ==> DepartmentDetailsToReturnDto
                var departments = _mapper.Map<DepartmentDetailsToReturnDto>(department);
                return departments;

            }
            return null;
        }

        public async Task<int> UpdateDepartmentAsync(UpdateDepartmentDto departmentDto)
        {
            //UpdateDepartmentDto ==> Department
            var depapartment = _mapper.Map<Department>(departmentDto);

            _unitOfWork.departmentRepository.Update(depapartment);
            return await _unitOfWork.CompleteAsync().ConfigureAwait(false);
        } 
        #endregion

    }
}
