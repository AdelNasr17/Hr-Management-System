using DataAccess.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Presentation_Layer.ViewModels.Roles;
using Presentation_Layer.ViewModels.Users;

namespace Presentation_Layer.Controllers.Roles
{
    //[Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _RoleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;

        // GetAll ,Get ,Update ,Delete
        //Index , Details ,Edit , Delete

        public RoleController(RoleManager<IdentityRole> RoleManager,UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
        {
            _RoleManager = RoleManager;
            _userManager = userManager;
            _env = env;
        }


        #region Index Action 

        [HttpGet]
        public async Task<IActionResult> Index(string SearchValue)
        {
            // Roles
            var RolesQueay = _RoleManager.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(SearchValue))
                RolesQueay = RolesQueay.Where(R => R.Name.ToLower().Contains(SearchValue.ToLower()));

            var RolesList = await RolesQueay.Select(R => new RoleViewModel()
            {
                Id = R.Id,
                Name = R.Name,
               

            }).ToListAsync().ConfigureAwait(false);
           
            return View(RolesList);

        }
        #endregion



        #region Create Action 

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel roleVM)
        {
            if(ModelState.IsValid)
            {
                await _RoleManager.CreateAsync(new IdentityRole()
                {
                    Name = roleVM.Name,
                }
                ).ConfigureAwait(false);
                return RedirectToAction(nameof(Index));
            }
            return View(roleVM);
        }

        #endregion


        #region Detalis Action
        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
                return BadRequest();
            var role = await _RoleManager.FindByIdAsync(id).ConfigureAwait(false);
            if (role == null)
                return NotFound();

            var roleVM = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name,
               
                
            };
            return View(roleVM);
        }
        #endregion

        #region Edit Action
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id is null)
                return BadRequest("Invalid Role ID.");

            var role = await _RoleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound("Role not found.");



            var users = await _userManager.Users.ToListAsync();
            return View(new RoleViewModel
            {
                Name = role.Name,
                Id = role.Id,
                Users = users.Select(U => new UserRoleViewModel
                {
                    UserId = U.Id,
                    UserName = U.UserName,
                    IsSelected = _userManager.IsInRoleAsync(U, role.Name).Result
                }).ToList()
            });

           
        }


        [HttpPost]
        [ValidateAntiForgeryToken] // Action Filter
        public async Task<IActionResult> Edit(string id, RoleViewModel roleVM)
        {
            if (!ModelState.IsValid)
                return View(roleVM);

            try
            {
                var role = await _RoleManager.FindByIdAsync(id);
                if (role == null)
                    return NotFound("Role not found.");
              
                role.Name = roleVM.Name;
            
                var Result = await _RoleManager.UpdateAsync(role);
                foreach (var userRole in roleVM.Users)
                {
                    var user = await _userManager.FindByIdAsync(userRole.UserId);
                    if (user is not null)
                    {
                        if(userRole.IsSelected && !( await _userManager.IsInRoleAsync(user, role.Name)))
                        {
                            await _userManager.AddToRoleAsync(user, role.Name);
                        }
                      else  if (!userRole.IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(user, role.Name);
                        }
                    }
                }
                if (Result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                    ModelState.AddModelError(string.Empty, "Role cannot be updated. Please check the details and try again.");
            }
            catch (Exception ex)
            {
                var message = _env.IsDevelopment() ? ex.Message : "An error occurred while updating the Rple.";
                ModelState.AddModelError(string.Empty, message);
            }

            return View(roleVM);
        }


        #endregion

        #region Delete Action
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id is null)
                return BadRequest();
            var role = await _RoleManager.FindByIdAsync(id).ConfigureAwait(false);
            if (role == null)
                return NotFound();
            return View(new RoleViewModel()
            {              
                Name = role.Name,
                Id = id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//Action Filter

        public async Task<IActionResult> ConfirmDelete(string id)
        {
            var role = await _RoleManager.FindByIdAsync(id).ConfigureAwait(false);
            var message = string.Empty;
            try
            {
                if (role is not null)
                {
                    await _RoleManager.DeleteAsync(role).ConfigureAwait(false);
                    return RedirectToAction(nameof(Index));
                }
                else
                {

                    message = "An Error Happend When Deleting The Role";

                }
            }
            catch (Exception ex)
            {

                message = _env.IsDevelopment() ? ex.Message : "An Error Happend When Deleting The Rple";


            }
            ModelState.AddModelError(string.Empty, message);
            return View(nameof(Index));
        }
        #endregion


    }
}
