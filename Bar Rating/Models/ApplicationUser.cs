using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Bar_Rating.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.EmailConfirmed = true;
            this.Id = Guid.NewGuid().ToString();
            this.Reviews = new List<Review>();
        }

        public ApplicationUser(string username, string firstName, string lastName)
        {
            this.EmailConfirmed = true;
            this.Id = Guid.NewGuid().ToString();
            this.UserName = username;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Reviews = new List<Review>();
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
