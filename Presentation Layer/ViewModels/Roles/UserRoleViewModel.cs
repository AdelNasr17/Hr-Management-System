namespace Presentation_Layer.ViewModels.Roles
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; } = null!;
        public string RoleId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public bool IsSelected { get; set; }
    }
}
