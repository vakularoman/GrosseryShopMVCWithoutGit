namespace AquaPlayground.ViewModels
{
    using AquaPlayground.Models;
    using Microsoft.AspNetCore.Identity;

    public class UserRoleEditViewModel
    {
        public long UserId { get; set; }

        public string UserEmail { get; set; }

        public List<Role> AllRoles { get; set; }

        public IList<string> UserRoles { get; set; }
    }
}
