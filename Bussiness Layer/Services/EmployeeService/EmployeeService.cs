using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bussiness_Layer.Comman.Services.AttachmentServices;

using DataAccess.Repositories.EmployeeRepository;
using DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IAttachmentService _attachmentService;

        //public EmployeeService(IEmployeeRepository employeeRepository,IMapper mapper)
        //{
        //    _employeeRepository = employeeRepository;
        //    _mapper = mapper;
        //}

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper,IAttachmentService attachmentService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _attachmentService = attachmentService;
        }

        public int AddEmployee(CreatedEmployeeDto EmployeeDto)
        {
            //CreateEmployeeDto ==> Department

            var EmployeeToCreated = _mapper.Map<Employee>(EmployeeDto);

            ///Employee employee = new Employee()
            ///{
            ///    Name = EmployeeDto.Name,
            ///    Age = EmployeeDto.Age,
            ///    Address = EmployeeDto.Address,
            ///    IsActive = EmployeeDto.IsActive,
            ///    Salary = EmployeeDto.Salary,
            ///    Email = EmployeeDto.Email,
            ///    PhoneNumber = EmployeeDto.PhoneNumber,
            ///    HiringDate = EmployeeDto.HiringDate,
            ///    Gender = EmployeeDto.Gender,
            ///    EmployeeType = EmployeeDto.EmployeeType,
            ///    CreatedBy = 1,
            ///    LastModifiedBy = 1,
            ///    CreatedOn=DateTime.UtcNow,
            ///    LastModifiedOn=DateTime.UtcNow,
            ///    DepartmentId=EmployeeDto.DepartmentID
            ///};
            

            if(EmployeeDto.ImageURL is not  null)
            {
                EmployeeToCreated.ImageURL = _attachmentService.Upload(EmployeeDto.ImageURL, "Images");
            }


            _unitOfWork.employeeRepository.Add(EmployeeToCreated);
            return _unitOfWork.Complete();
        }

        public bool DeleteEmployee(int id)
        {
            var EmployeeRepo= _unitOfWork.employeeRepository;
           var employee = EmployeeRepo.GetById(id);
            if (employee is not null)
                EmployeeRepo.Remove(employee);
            return _unitOfWork.Complete()>0;
        }

        public IEnumerable<EmployeeToReturnDto> GetAllEmployees(string SearchValue)
        {
            //Employee==> EmployeeToReturnDto

            return _unitOfWork.employeeRepository.GetAllQueryable().Include(E => E.Department)
                .Where(E => E.IsDeleted == false && (string.IsNullOrEmpty(SearchValue)) || E.Name.ToLower().Contains(SearchValue))
                .ProjectTo<EmployeeToReturnDto>(_mapper.ConfigurationProvider);
            //    .Select(employee => new EmployeeToReturnDto
            //{
            //    Id=employee.Id,
            //    Name=employee.Name,
            //    Salary  =employee.Salary,
            //    Email =employee.Email,
            //    Age = employee.Age,
            //    Gender=employee.Gender.ToString(),
            //    EmployeeType=employee.EmployeeType.ToString(),
            //    IsActive=employee.IsActive,
            //    Department = employee.Department.Name //Use Eager Loading


            //});
        }

        public EmployeeDetailsToReturnDto? GetEmployeeById(int id)
        {

            var employee = _unitOfWork.employeeRepository.GetById(id) ;
            if(employee is not null)
            {

                // Employee==> EmployeeDetailsToReturnDto
                return _mapper.Map<EmployeeDetailsToReturnDto>(employee);
                //    new EmployeeDetailsToReturnDto()
                //{
                //    Id = employee.Id,
                //    Name = employee.Name,
                //    Salary = employee.Salary,
                //    Email = employee.Email,
                //    Address = employee.Address,
                //    HiringDate = employee.HiringDate,
                //    PhoneNumber = employee.PhoneNumber,
                //    Age = employee.Age,
                //    Gender = employee.Gender.ToString(),
                //    EmployeeType = employee.EmployeeType.ToString(),
                //    IsActive = employee.IsActive,
                //    CreatedBy = employee.CreatedBy,
                //    CreatedOn = employee.CreatedOn,
                //    LastModifiedBy = employee.LastModifiedBy,
                //    LastModifiedOn = employee.LastModifiedOn,
                //    IsDeleted = employee.IsDeleted,
                //    DepartmentName = employee.Department?.Name //Once Access Nev Property [Lazy Loading ]

                //};
               
            }
            return null;
        }

        public int UpdateEmployee(UpdateEmployeeDto EmployeeDto)
        {
            // UpdateEmployeeDto=> Employee
            var employee = _mapper.Map<Employee>(EmployeeDto);
            //Employee employee = new Employee()
            //{
            //    Id = EmployeeDto.Id,
            //    Name = EmployeeDto.Name,
            //    Age = EmployeeDto.Age,
            //    Address = EmployeeDto.Address,
            //    IsActive = EmployeeDto.IsActive,
            //    Salary = EmployeeDto.Salary,
            //    Email = EmployeeDto.Email,
            //    PhoneNumber = EmployeeDto.PhoneNumber,
            //    HiringDate = EmployeeDto.HiringDate,
            //    Gender = EmployeeDto.Gender,
            //    EmployeeType = EmployeeDto.EmployeeType,
            //    CreatedBy = 1,
            //    LastModifiedBy = 1,
            //    CreatedOn = DateTime.UtcNow,
            //    LastModifiedOn = DateTime.UtcNow,
            //    DepartmentId = EmployeeDto.DepartmentId
            //};
            _unitOfWork.employeeRepository.Update(employee);
            return _unitOfWork.Complete();
        }





    }
}
