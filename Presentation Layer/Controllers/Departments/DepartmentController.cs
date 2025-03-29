using Bussiness_Layer.Data_Transfer_Object.Department;
using Bussiness_Layer.Services.DepartmentService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Presentation_Layer.ViewModels.Departments;

namespace Presentation_Layer.Controllers.Departments
 { 
    public class DepartmentController : Controller
    {
        #region Services

        private readonly IDepartmentService _departmentServices;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger, IWebHostEnvironment env)
        {
            _departmentServices = departmentService;
            _logger = logger;
            _env = env;
        }
        #endregion

        #region Index Action
        //VioewStorage ==> ViewDate , ViewBag ==> deal With She Same Storage
        //Dictonary 
        //Extra Data
        //1) Send Data From Action In Controller To View 
        //2) Send Data From view To Partial View 
        //3) Send Data From view To Partial Layout 
        

        //View Data => .Net 3.5
        //View Bag => >Net 4.0

        //TempData ==> Send Data from Request To Anther Request [From Action To Anther Action ]
        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentServices.GetAllDepartments();

            return View(departments);
        }
        #endregion

        #region Create Action
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]//Action Filter

        public IActionResult Create(DepartmentViewModel departmentVM)
        {
            if (!ModelState.IsValid)
                return View(departmentVM);
            var message = string.Empty;
            try
            {
                var Result = _departmentServices.AddDepartment(new CreatedDepartmentDto()
                {
                  
                    Code = departmentVM.Code,
                    Name = departmentVM.Name,
                    CreationDate = departmentVM.CreationDate,
                    Description = departmentVM.Description,
                });
                if (Result > 0)
                {
                    TempData["Message"] = "Department Is Created";
                    return RedirectToAction(nameof(Index));
                }
                else
                {

                    message = "Department Can Not be Created";
                    TempData["Message"] = message;
                    ModelState.AddModelError(string.Empty, message);
                    return View(departmentVM);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                if (_env.IsDevelopment())
                {
                    message = ex.Message;
                    return View(departmentVM);
                }
                else
                {
                    message = "Department Can Not be Created";
                    return View("Error", message);
                }
            }

        }

        #endregion

        #region Details Action
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest();
            var department = _departmentServices.GetDepartmentById(id.Value);
            if (department == null)
                return NotFound();

            return View(department);
        }
        #endregion

        #region Edit Action
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return BadRequest();
            var department = _departmentServices.GetDepartmentById(id.Value);
            if (department == null)
                return NotFound();
            var UpdateDept = new DepartmentViewModel
            {
                Code = department.Code,
                Name = department.Name,
                CreationDate = department.CreatedOn,
                Description = department.Description,
            };
            return View(UpdateDept);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//Action Filter

        public IActionResult Edit(int id, DepartmentViewModel departmendVM)
        {
            if (!ModelState.IsValid)
                return View(departmendVM);
            var message = string.Empty;
            try
            {
                var Result = _departmentServices.UpdateDepartment(new UpdateDepartmentDto
                {
                    Id = id,
                    Code = departmendVM.Code,
                    Name = departmendVM.Name,
                    CreationDate = departmendVM.CreationDate,
                    Description = departmendVM.Description,
                });
                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {

                    message = "Department Can Not be Created";

                }

            }
            catch (Exception ex)
            {
                message = _env.IsDevelopment() ? ex.Message : "Department Can Not be Created";
            }
            return View(departmendVM);
        }
        #endregion

        #region Delete Action
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = _departmentServices.GetDepartmentById(id.Value);
            if (department == null)
                return NotFound();
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//Action Filter

        public IActionResult Delete(int id)
        {
            var Result = _departmentServices.DeleteDepartment(id);
            var message = string.Empty;
            try
            {
                if (Result == true)
                    return RedirectToAction(nameof(Index));
                else
                {

                    message = "An Error Happend When Deleting The Department";

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _env.IsDevelopment() ? ex.Message : "An Error Happend When Deleting The Department";


            }
            ModelState.AddModelError(string.Empty, message);
            return View(nameof(Index));
        } 
        #endregion

    }
}
