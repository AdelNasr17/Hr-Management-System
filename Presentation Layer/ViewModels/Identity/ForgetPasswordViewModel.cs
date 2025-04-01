using System.ComponentModel.DataAnnotations;

namespace Presentation_Layer.ViewModels.Identity
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; } 

    }
}
