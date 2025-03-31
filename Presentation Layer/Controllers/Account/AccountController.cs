using DataAccess.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation_Layer.ViewModels.Identity;

namespace Presentation_Layer.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            _UserManager = userManager;
            _signInManager = signInManager;
        }


        // Register , Login , Signnout
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {

            if (ModelState.IsValid)
            {
                var User = new ApplicationUser()
                {
                    UserName = registerViewModel.Email.Split('@')[0],
                    Email = registerViewModel.Email,
                    FName = registerViewModel.FName,
                    LName = registerViewModel.LName,
                    IsAgree = registerViewModel.IsAgree,
                };
            var Result= await   _UserManager.CreateAsync(User,registerViewModel.Password).ConfigureAwait(false);

                if(Result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach(var error in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }
                return View(registerViewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var User = await _UserManager.FindByEmailAsync(loginViewModel.Email).ConfigureAwait(false);
                if (User is not null)
                {
                  var flag = await  _UserManager.CheckPasswordAsync(User,loginViewModel.Password).ConfigureAwait(false);
                    if(flag==true)//Email Exist & Password Exist
                    {
                        // Token ==> encrypted string lhkjbjlkbnlklkb;kblkj
                       var Result =await _signInManager.PasswordSignInAsync(User, loginViewModel.Password, loginViewModel.RememberMe, false).ConfigureAwait(false);
                        if(Result.Succeeded)                      
                            return RedirectToAction("Index", "Home");
                    }
                    else //Email Exist but Password Not Exist                    
                        ModelState.AddModelError(string.Empty, "Password is not incorrect");
                }
                else               
                    ModelState.AddModelError(string.Empty, "Email Is Not Found");                
            }

            return View (loginViewModel);
        }


        [HttpGet]
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync().ConfigureAwait(false);
            return RedirectToAction(nameof(Login));
        }


    }
}
