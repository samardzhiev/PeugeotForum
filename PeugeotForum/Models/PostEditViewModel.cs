namespace PeugeotForum.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PostEditViewModel
    {
        [Required]
        public int PostId { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "The length of the content should be at least 10 characters!")]
        [MaxLength(1000, ErrorMessage = "The length of the content should be less then 1000 characters!")]
        public string Content { get; set; }
    }
}