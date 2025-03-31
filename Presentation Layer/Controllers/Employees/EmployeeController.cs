using AutoMapper;
using Bussiness_Layer.Data_Transfer_Object.Department;
using Bussiness_Layer.Data_Transfer_Object.Employee;
using Bussiness_Layer.Services.DepartmentService;
using Bussiness_Layer.Services.EmployeeService;
using DataAccess.Models.Employee;
using DataAccess.Models.Shared.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.VisualBasic;
using Presentation_Layer.ViewModels.Employees;



namespace Presentation_Layer.Controllers.Employees
{
    [Route("Employees/[controller]/ [action]")]
    [Authorize]
    public class EmployeeController : Controller
    {
        #region Services
        private readonly IEmployeeService _employeeServices;
        private readonly IMapper _mapper;

        //private readonly IDepartmentService _departmentService;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeService employeeService,IMapper mapper,ILogger<EmployeeController> logger, IWebHostEnvironment env)
        {
            _employeeServices = employeeService;
            _mapper = mapper;
            //_departmentService = departmentService;
            _logger = logger;
            _env = env;
        } 
        #endregion

        #region Index Action
        [HttpGet]
        public async Task< IActionResult> Index(string SearchValue)
        {
            var Employees = await _employeeServices.GetAllEmployeesAsync(SearchValue).ConfigureAwait(false);

            return View(Employees);
        }
        #endregion

        #region Create Action
        [HttpGet]
        public IActionResult Create(/*[FromServices] IDepartmentService _departmentService*/)
        {
            //ViewData["Departments"] = _departmentService.GetAllDepartments();
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]//Action Filter
        public async Task< IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (!ModelState.IsValid)
                return View(employeeVM);
            var message = string.Empty;
            try
            {
                // EmployeeViewModel => CreateEmployeeDto
                var EmployeeCreated =_mapper.Map<CreatedEmployeeDto>(employeeVM);
                var Result =await _employeeServices.AddEmployeeAsync(EmployeeCreated).ConfigureAwait(false);
                //    new CreatedEmployeeDto()
                //    {
                    
                //    Name=employeeVM.Name,
                //    Salary=employeeVM.Salary,
                //    Age=employeeVM.Age,
                //    PhoneNumber=employeeVM.PhoneNumber,
                //    Address=employeeVM.Address,
                //    Email=employeeVM.Email,
                //    IsActive=employeeVM.IsActive,
                //    HiringDate=employeeVM.HiringDate,
                //    Gender = Enum.TryParse(employeeVM.Gender, out Gender gender) ? gender : default,
                //    EmployeeType = Enum.TryParse(employeeVM.EmployeeType, out EmployeeType empType) ? empType : default,
                //    DepartmentID = employeeVM.ID,
                //});
   
                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "Employee Can Not be Created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(employeeVM);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                if (_env.IsDevelopment())
                {
                    message = ex.Message;
                    return View(employeeVM);
                }
                else
                {
                    message = "Employee Can Not be Created";
                    return View("Error", message);
                }
            }

        }

        #endregion


        #region Detalis Action
        [HttpGet]
        public async Task< IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest();
            var Employee =await _employeeServices.GetEmployeeByIdAsync(id.Value).ConfigureAwait(false);
            if (Employee == null)
                return NotFound();

            return View(Employee);
        }
        #endregion

        #region Edit Action
        [HttpGet]
        public async Task< IActionResult> Edit(int? id)
        {
            if (id == null )
                return BadRequest("Invalid employee ID.");

            var employee =await _employeeServices.GetEmployeeByIdAsync(id.Value).ConfigureAwait(false);
            if (employee == null)
                return NotFound("Employee not found.");


            //EmployeeDetailsToReturnDto=> EmployeViewModel
            var updateEmployeeDto = _mapper.Map<EmployeeViewModel>(employee);

            return View(updateEmployeeDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken] // Action Filter
        public async Task< IActionResult> Edit(int id, EmployeeViewModel employeeVM)
        {
            if (!ModelState.IsValid)
                return View(employeeVM);

            try
            {
                //EmployeeViewModel => UpdateEmployeeDto
            var EmployeeToUpdate= _mapper.Map<UpdateEmployeeDto>(employeeVM);              
                EmployeeToUpdate.Id = id;
                var result =await _employeeServices.UpdateEmployeeAsync(EmployeeToUpdate).ConfigureAwait(false);
                if (result > 0)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, "Employee cannot be updated. Please check the details and try again.");
            }
            catch (Exception ex)
            {
                var message = _env.IsDevelopment() ? ex.Message : "An error occurred while updating the employee.";
                ModelState.AddModelError(string.Empty, message);
            }

            return View(employeeVM);
        }


        #endregion

        #region Delete Action
        [HttpGet]
        public async Task< IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var Employee =await _employeeServices.GetEmployeeByIdAsync(id.Value).ConfigureAwait(false);
            if (Employee == null)
                return NotFound();
            return View(Employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//Action Filter

        public async Task< IActionResult> Delete(int id)
        {
            var Result = await _employeeServices.DeleteEmployeeAsync(id).ConfigureAwait(false);
            var message = string.Empty;
            try
            {
                if (Result == true)
                    return RedirectToAction(nameof(Index));
                else
                {

                    message = "An Error Happend When Deleting The Employee";

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _env.IsDevelopment() ? ex.Message : "An Error Happend When Deleting The Employee";


            }
            ModelState.AddModelError(string.Empty, message);
            return View(nameof(Index));
        } 
        #endregion
    }
}
