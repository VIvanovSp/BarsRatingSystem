using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;

namespace Bar_Rating.Models
{
    public class BarViewModel
    {
        public BarViewModel()
        {
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        [BindProperty]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        [BindProperty]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Image")]
        [BindProperty]
        public IFormFile BarImage { get; set; }
    }
}
