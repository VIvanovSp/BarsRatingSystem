using System.ComponentModel.DataAnnotations;

namespace Bar_Rating.Models
{
    public class Bar
    {
        public Bar()
        {
            this.Reviews = new List<Review>();
        }

        public Bar(string name, string description, byte[] barImage)
        {
            this.Reviews = new List<Review>();
            this.Name=name;
            this.Description=description;
            this.BarImage=barImage;
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        // 2097152 bytes = 2 mb
        [Required]
        [MaxLength(2097152)]
        public byte[] BarImage { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
