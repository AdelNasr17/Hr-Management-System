namespace Presentation_Layer.ViewModels.Roles
{
    public class RoleViewModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;

        public List<UserRoleViewModel> Users { get; set; }= new List<UserRoleViewModel>();

        public RoleViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
