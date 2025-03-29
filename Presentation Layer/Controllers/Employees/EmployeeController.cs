using Bussiness_Layer.Data_Transfer_Object.Employee;
using Bussiness_Layer.Services.DepartmentService;
using Bussiness_Layer.Services.EmployeeService;
using DataAccess.Models.Employee;
using DataAccess.Models.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;



namespace Presentation_Layer.Controllers.Employees
{
    [Route("Employees/[controller]/ [action]")]
    public class EmployeeController : Controller
    {
        #region Services
        private readonly IEmployeeService _employeeServices;
        //private readonly IDepartmentService _departmentService;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeService employeeService,ILogger<EmployeeController> logger, IWebHostEnvironment env)
        {
            _employeeServices = employeeService;
            //_departmentService = departmentService;
            _logger = logger;
            _env = env;
        } 
        #endregion

        #region Index Action
        [HttpGet]
        public IActionResult Index()
        {
            var Employees = _employeeServices.GetAllEmployees();

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
        public IActionResult Create(CreatedEmployeeDto employeeVM)
        {
            if (!ModelState.IsValid)
                return View(employeeVM);
            var message = string.Empty;
            try
            {
                var Result = _employeeServices.AddEmployee(employeeVM);
   
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
        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest();
            var Employee = _employeeServices.GetEmployeeById(id.Value);
            if (Employee == null)
                return NotFound();

            return View(Employee);
        }
        #endregion

        #region Edit Action
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id <= 0)
                return BadRequest("Invalid employee ID.");

            var employee = _employeeServices.GetEmployeeById(id.Value);
            if (employee == null)
                return NotFound("Employee not found.");

            var updateEmployeeDto = new UpdateEmployeeDto
            {
                EmployeeType = Enum.TryParse(employee.EmployeeType, out EmployeeType empType) ? empType : default, 
                Gender = Enum.TryParse(employee.Gender, out Gender gender) ? gender : default,
                Age = employee.Age,
                Email = employee.Email,
                IsActive = employee.IsActive,
                Address = employee.Address,
                Name = employee.Name,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = employee.HiringDate,
                Id = id.Value,
                Salary = employee.Salary,
            };

            return View(updateEmployeeDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken] // Action Filter
        public IActionResult Edit(int id, UpdateEmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
                return View(employeeDto);

            try
            {
                var result = _employeeServices.UpdateEmployee(employeeDto);
                if (result > 0)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, "Employee cannot be updated. Please check the details and try again.");
            }
            catch (Exception ex)
            {
                var message = _env.IsDevelopment() ? ex.Message : "An error occurred while updating the employee.";
                ModelState.AddModelError(string.Empty, message);
            }

            return View(employeeDto);
        }
        #endregion

        #region Delete Action
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var Employee = _employeeServices.GetEmployeeById(id.Value);
            if (Employee == null)
                return NotFound();
            return View(Employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//Action Filter

        public IActionResult Delete(int id)
        {
            var Result = _employeeServices.DeleteEmployee(id);
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
