using Microsoft.AspNetCore.Identity;

namespace Bar_Rating.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string roleName)
            : base(roleName)
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
