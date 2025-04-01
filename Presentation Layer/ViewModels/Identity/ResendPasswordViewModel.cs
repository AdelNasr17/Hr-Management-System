using System.ComponentModel.DataAnnotations;

namespace Presentation_Layer.ViewModels.Identity
{
    public class ResendPasswordViewModel
    {
        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "ConfirmPassword Is Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Doesn't Match")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
