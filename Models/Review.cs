using System.ComponentModel.DataAnnotations;

namespace Bar_Rating.Models
{
    public class Review
    {
        public Review()
        {
        }

        public Review(string description, string userId, int barId)
        {
            this.Description = description;
            this.UserId = userId;
            this.BarId = barId;
        }

        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        public int BarId { get; set; }
        public Bar Bar { get; set; }
    }
}
