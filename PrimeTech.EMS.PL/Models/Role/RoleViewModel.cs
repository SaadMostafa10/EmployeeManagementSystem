namespace PrimeTech.EMS.PL.Models.Role
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<UserRoleViewModel> Users { get; set; } = new List<UserRoleViewModel>();
        public RoleViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
