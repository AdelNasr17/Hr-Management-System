using System.ComponentModel.DataAnnotations;

namespace Presentation_Layer.ViewModels.Identity
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First Name Is Required")]
        public string FName { get; set; } = null!;
        [Required(ErrorMessage = "Last Name Is Required")]
        public string LName { get; set; }= null!;
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }= null!;
        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }= null!;
        [Required(ErrorMessage = "ConfirmPassword Is Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Doesn't Match")]
        public string ConfirmPassword { get; set; }=null!;

        public bool IsAgree { get; set; }
    }
}
