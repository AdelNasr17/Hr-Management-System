using Bussiness_Layer.Data_Transfer_Object.Employee;
using DataAccess.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation_Layer.ViewModels.Employees;
using Presentation_Layer.ViewModels.Users;

namespace Presentation_Layer.Controllers.Users
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;

        // GetAll ,Get ,Update ,Delete
        //Index , Details ,Edit , Delete

        public UserController(UserManager<ApplicationUser> userManager,IWebHostEnvironment env)
        {
            _userManager = userManager;
            _env = env;
        }


        #region Index Action 

        [HttpGet]
        public async Task<IActionResult> Index(string SearchValue)
        {
            // Users
            var userQueay = _userManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(SearchValue))
                userQueay = userQueay.Where(U => U.Email.ToLower().Contains(SearchValue.ToLower()));

            var usersList = await userQueay.Select(u => new UserViewModel()
            {
                Id = u.Id,
                FName = u.FName,
                LName = u.LName,
                Email = u.Email,

            }).ToListAsync().ConfigureAwait(false);

            foreach (var user in usersList)
                user.Roles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(user.Id)).ConfigureAwait(false);

            return View(usersList);

        }
        #endregion


        #region Detalis Action
        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
                return BadRequest();
            var user = await _userManager.FindByIdAsync(id).ConfigureAwait(false);
            if (user == null)
                return NotFound();

            var userVM = new UserViewModel()
            {
                Id = user.Id,
                FName = user.FName,
                LName = user.LName,
                Email = user.Email,
                Roles =  _userManager.GetRolesAsync(user).Result,
            };
            return View(userVM);
        }
        #endregion

        #region Edit Action
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
                return BadRequest("Invalid User ID.");

            var user = await _userManager.FindByIdAsync(id).ConfigureAwait(false);
            if (user == null)
                return NotFound("User not found.");


            //EmployeeDetailsToReturnDto=> EmployeViewModel
            var updateUserVM = new UserViewModel()
                {
                Email=user.Email,
                FName=user.FName,
                LName=user.LName,
                Id=user.Id,
                Roles=_userManager.GetRolesAsync(user).Result,


            };

            return View(updateUserVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken] // Action Filter
        public async Task<IActionResult> Edit(string id, UserViewModel userVM)
        {
            if (!ModelState.IsValid)
                return View(userVM);

            try
            {
                var user = await _userManager.FindByIdAsync(id).ConfigureAwait (false);
                if (user == null)
                    return NotFound("User not found.");
                user.FName = userVM.FName;
                user.LName = userVM.LName;
                
                user.Email=userVM.Email;

              var Result= await _userManager.UpdateAsync(user).ConfigureAwait(false);
                if (Result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                ModelState.AddModelError(string.Empty, "User cannot be updated. Please check the details and try again.");
            }
            catch (Exception ex)
            {
                var message = _env.IsDevelopment() ? ex.Message : "An error occurred while updating the user.";
                ModelState.AddModelError(string.Empty, message);
            }

            return View(userVM);
        }


        #endregion

        #region Delete Action
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id is null)
                return BadRequest();
            var user = await _userManager.FindByIdAsync(id).ConfigureAwait(false);
            if (user == null)
                return NotFound();
            return View(new UserViewModel()
            {
                Email=user.Email,
                FName=user.FName,
                LName=user.LName,
                Id=id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//Action Filter

        public async Task<IActionResult> ConfirmDelete(string id)
        {
            var user = await _userManager.FindByIdAsync(id).ConfigureAwait(false);
            var message = string.Empty;
            try
            {
                if (user is not null)
                {
                    await _userManager.DeleteAsync(user).ConfigureAwait(false);
                    return RedirectToAction(nameof(Index));
                }
                else
                {

                    message = "An Error Happend When Deleting The User";

                }
            }
            catch (Exception ex)
            {
                
                message = _env.IsDevelopment() ? ex.Message : "An Error Happend When Deleting The User";


            }
            ModelState.AddModelError(string.Empty, message);
            return View(nameof(Index));
        }
        #endregion




    }
}
