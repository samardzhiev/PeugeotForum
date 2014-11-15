namespace PeugeotForum.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Post
    {
        public Post()
        {
            this.CreatedOn = DateTime.Now;
        }
        public int PostId { get; set; }

        [Required]
        [MinLength(10,ErrorMessage="The length of the content should be at least 10 characters!")]
        [MaxLength(1000, ErrorMessage="The length of the content should be less then 1000 characters!")]
        public string Content { get; set; }

        [Required]
        public int Likes { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public int TopicId { get; set; }

        [Required]
        public virtual Topic Topic { get; set; }
    }
}
