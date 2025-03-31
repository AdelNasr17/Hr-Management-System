


namespace Bussiness_Layer.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        #region Services
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IAttachmentService _attachmentService;

        ///public EmployeeService(IEmployeeRepository employeeRepository,IMapper mapper)
        ///{
        ///    _employeeRepository = employeeRepository;
        ///    _mapper = mapper;
        ///}

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper, IAttachmentService attachmentService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _attachmentService = attachmentService;
        }

        #endregion


        #region Method

        public async Task<int> AddEmployeeAsync(CreatedEmployeeDto EmployeeDto)
        {
            //CreateEmployeeDto ==> Department
            var EmployeeToCreated = _mapper.Map<Employee>(EmployeeDto);
            if (EmployeeDto.Image is not null)
            {
                EmployeeToCreated.ImageURL = await _attachmentService.UploadAsync(EmployeeDto.Image, "Images").ConfigureAwait(false);
            }

            _unitOfWork.employeeRepository.Add(EmployeeToCreated);
            return await _unitOfWork.CompleteAsync().ConfigureAwait(false);
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var EmployeeRepo = _unitOfWork.employeeRepository;
            var employee = await EmployeeRepo.GetByIdAsync(id).ConfigureAwait(false);
            if (employee is not null)
                EmployeeRepo.Remove(employee);
            return await _unitOfWork.CompleteAsync().ConfigureAwait(false) > 0;
        }

        public async Task<IEnumerable<EmployeeToReturnDto>> GetAllEmployeesAsync(string SearchValue)
        {
            //Employee==> EmployeeToReturnDto
            return await _unitOfWork.employeeRepository.GetAllQueryable().Include(E => E.Department)
               .Where(E => E.IsDeleted == false && (string.IsNullOrEmpty(SearchValue)) || E.Name.ToLower().Contains(SearchValue))
               .ProjectTo<EmployeeToReturnDto>(_mapper.ConfigurationProvider).AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task<EmployeeDetailsToReturnDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _unitOfWork.employeeRepository.GetByIdAsync(id).ConfigureAwait(false);
            if (employee is not null)
            {
                // Employee==> EmployeeDetailsToReturnDto
                return _mapper.Map<EmployeeDetailsToReturnDto>(employee);

            }
            return null;
        }

        public async Task<int> UpdateEmployeeAsync(UpdateEmployeeDto EmployeeDto)
        {
            // UpdateEmployeeDto=> Employee
            var employee = _mapper.Map<Employee>(EmployeeDto);

            _unitOfWork.employeeRepository.Update(employee);
            return await _unitOfWork.CompleteAsync().ConfigureAwait(false);
        }

        #endregion





    }
}
