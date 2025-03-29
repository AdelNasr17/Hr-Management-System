using DataAccess.Repositories.EmployeeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public int AddEmployee(CreatedEmployeeDto EmployeeDto)
        {
            Employee employee = new Employee()
            {
                Name = EmployeeDto.Name,
                Age = EmployeeDto.Age,
                Address = EmployeeDto.Address,
                IsActive = EmployeeDto.IsActive,
                Salary = EmployeeDto.Salary,
                Email = EmployeeDto.Email,
                PhoneNumber = EmployeeDto.PhoneNumber,
                HiringDate = EmployeeDto.HiringDate,
                Gender = EmployeeDto.Gender,
                EmployeeType = EmployeeDto.EmployeeType,
                CreatedBy = 1,
                LastModifiedBy = 1,
                CreatedOn=DateTime.UtcNow,
                LastModifiedOn=DateTime.UtcNow,
                DepartmentId=EmployeeDto.DepartmentID
                


            };
            return _employeeRepository.Add(employee);
        }

        public bool DeleteEmployee(int id)
        {
           var employee = _employeeRepository.GetById(id);
            if (employee is not null)
            return _employeeRepository.Remove(employee)>0 ;

            return false;
        }

        public IEnumerable<EmployeeToReturnDto> GetAllEmployees()
        {
            return _employeeRepository.GetAllQueryable().Where(E=> E.IsDeleted ==false).Select(employee => new EmployeeToReturnDto
            {
                Id=employee.Id,
                Name=employee.Name,
                Salary  =employee.Salary,
                Email =employee.Email,
                Age = employee.Age,
                Gender=employee.Gender.ToString(),
                EmployeeType=employee.EmployeeType.ToString(),
                IsActive=employee.IsActive,
                Department = employee.Department.Name //Use Eager Loading


            });
        }

        public EmployeeDetailsToReturnDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id) ;
            if(employee is not null)
            {
                return new EmployeeDetailsToReturnDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Salary = employee.Salary,
                    Email = employee.Email,
                    Address = employee.Address,
                    HiringDate = employee.HiringDate,
                    PhoneNumber = employee.PhoneNumber,
                    Age = employee.Age,
                    Gender = employee.Gender.ToString(),
                    EmployeeType = employee.EmployeeType.ToString(),
                    IsActive = employee.IsActive,
                    CreatedBy = employee.CreatedBy,
                    CreatedOn = employee.CreatedOn,
                    LastModifiedBy = employee.LastModifiedBy,
                    LastModifiedOn = employee.LastModifiedOn,
                    IsDeleted = employee.IsDeleted,
                    Department = employee.Department?.Name //Once Access Nev Property [Lazy Loading ]

                };
               
            }
            return null;
        }

        public int UpdateEmployee(UpdateEmployeeDto EmployeeDto)
        {
            Employee employee = new Employee()
            {
                Id = EmployeeDto.Id,
                Name = EmployeeDto.Name,
                Age = EmployeeDto.Age,
                Address = EmployeeDto.Address,
                IsActive = EmployeeDto.IsActive,
                Salary = EmployeeDto.Salary,
                Email = EmployeeDto.Email,
                PhoneNumber = EmployeeDto.PhoneNumber,
                HiringDate = EmployeeDto.HiringDate,
                Gender = EmployeeDto.Gender,
                EmployeeType = EmployeeDto.EmployeeType,
                CreatedBy = 1,
                LastModifiedBy = 1,
                CreatedOn = DateTime.UtcNow,
                LastModifiedOn = DateTime.UtcNow,
                      


            };
         return   _employeeRepository.Update(employee);
        }
    }
}
