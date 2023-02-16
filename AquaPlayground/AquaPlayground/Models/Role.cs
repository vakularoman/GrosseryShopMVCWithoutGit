namespace AquaPlayground.Models
{
    using Microsoft.AspNetCore.Identity;

    public class Role : IdentityRole<long>
    {
        public Role()
            : base()
        {
        }

        public Role(string roleName)
            : base(roleName)
        {
        }
    }
}
