using Bussiness_Layer.Services.EmailService;
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
        private readonly IEmailService _emailService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IEmailService emailService )
        {
            _UserManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }


        // Register , Login , SignOut
        #region Register Action
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
                var Result = await _UserManager.CreateAsync(User, registerViewModel.Password).ConfigureAwait(false);

                if (Result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }
            return View(registerViewModel);
        }
        #endregion


        #region Login Action
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
                    var flag = await _UserManager.CheckPasswordAsync(User, loginViewModel.Password).ConfigureAwait(false);
                    if (flag == true)//Email Exist & Password Exist
                    {
                        // Token ==> encrypted string lhkjbjlkbnlklkb;kblkj
                        var Result = await _signInManager.PasswordSignInAsync(User, loginViewModel.Password, loginViewModel.RememberMe, false).ConfigureAwait(false);
                        if (Result.Succeeded)
                            return RedirectToAction("Index", "Home");
                    }
                    else //Email Exist but Password Not Exist                    
                        ModelState.AddModelError(string.Empty, "Password is not incorrect");
                }
                else
                    ModelState.AddModelError(string.Empty, "Email Is Not Found");
            }

            return View(loginViewModel);
        }

        #endregion


        #region SignOut Action
        [HttpGet]
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync().ConfigureAwait(false);
            return RedirectToAction(nameof(Login));
        }
        #endregion


        #region Forget Password

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            if(ModelState.IsValid)
            {
                var user = await _UserManager.FindByEmailAsync(forgetPasswordViewModel.Email);
                if(user is not null)
                {
                    var token=await _UserManager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);
                    //Account/RessentPassword?email=adel@gmail.com
                    var url = Url.Action("ResetPassword", "Account", new { email = forgetPasswordViewModel.Email,token=token },Request.Scheme);
                    //baseUrl/
                    // To ,Subject , Body

                    var email = new Email()
                    {
                        To = forgetPasswordViewModel.Email,
                        Subject = "Reset your Password",
                        Body=url//URL
                    };

                    //Send Email
                    _emailService.SendEmail(email);
                    return RedirectToAction("CheckYourIndex");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Operation ,Please try again ");
                }
            }

            return View(forgetPasswordViewModel);
        }
       

        [HttpGet]

        public IActionResult CheckYourIndex()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string email,string token)
        {
            //pass
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResendPasswordViewModel resendPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;
                var user = await _UserManager.FindByEmailAsync(email).ConfigureAwait(false);
                if(user != null)
                {
                  var Result= await  _UserManager.ResetPasswordAsync(user, token,resendPasswordViewModel.Password).ConfigureAwait(false);

                    if(Result.Succeeded)
                    {
                        return RedirectToAction(nameof(Login));
                    }

                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Operation P;ease Try again");
            return View (resendPasswordViewModel);

        }
        #endregion
    }
}
