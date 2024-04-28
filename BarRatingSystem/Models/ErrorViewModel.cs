using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarRatingSystem.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class Review
    {
        private int id;

        private string userId;

        private ApplicationUser user;

        private int barId;

        private Bar bar;

        private string content;

        private DateTime createdDate = DateTime.Now;

        public Review()
        {

        }

        [Key]  // Designates this property as the primary key in the database
        public int Id { get => id; set => id = value; }

        [Required]  // Ensures that the UserId must be provided
        public string UserId { get => userId; set => userId = value; }  // Foreign key for ApplicationUser

        public ApplicationUser User { get => user; set => user = value; }

        [Required]  // Ensures that the BarId must be provided
        public int BarId { get => barId; set => barId = value; }  // Foreign key for the Bar

        [ForeignKey(nameof(BarId))]
        public Bar Bar { get => bar; set => bar = value; }

        [Required(ErrorMessage = "Review content cannot be empty.")]
        [StringLength(1000, ErrorMessage = "The content must not exceed 1000 characters.")]
        public string Content { get => content; set => content = value; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get => createdDate; set => createdDate = value; }
    }


    public class Bar
    {
        private int id;

        private string name;

        private string description;

        private byte[] image;

        public Bar()
        {

        }

        [Key] // Indicates that Id is the primary key
        public int Id { get => id; set => id = value; }

        [Required(ErrorMessage = "The name of the bar is required.")]
        [StringLength(64, ErrorMessage = "The name must be less than 64 characters long.")]
        public string Name { get => name; set => name = value; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(255, ErrorMessage = "The description must be less than 255 characters long.")]
        public string Description { get => description; set => description = value; }

        // You might consider using a custom validation attribute here to ensure file size limits or specific formats
        [Display(Name = "Bar Image")]
        [DataType(DataType.Upload)]
        [MaxFileSize(2 * 1024 * 1024, ErrorMessage = "The image must be less than 2 MB.")]
        public byte[] Image { get => image; set => image = value; }

        [Required(ErrorMessage = "An image is required.")]
        [NotMapped] // This property will not be mapped to any database column
        public IFormFile UploadedImage { get; set; }
    }

    /// <summary>
    /// Custom validation attribute to check the maximum file size.
    /// </summary>
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as byte[];
            if (file != null)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Maximum allowed file size is {_maxFileSize} bytes.";
        }
    }
}