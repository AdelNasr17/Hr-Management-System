namespace Presentation_Layer.ViewModels.Users
{
    public class UserViewModel
    {
        public string Id { get; set; } = null!;
        public string FName { get; set; } = null!;
        public string LName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public IEnumerable<string> Roles { get; set; }= new List<string>();
        //RelationShip Betrween User And Role Tables ==> Many To Many

    }
}
