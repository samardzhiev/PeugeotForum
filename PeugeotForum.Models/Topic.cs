namespace PeugeotForum.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Topic
    {
        public Topic()
        {
            this.Posts = new HashSet<Post>();
            this.DateCreated = DateTime.Now;
        }

        public int TopicId { get; set; }

        [Required]
        [MinLength(10, ErrorMessage="The length of the title should be at least 10 characters!")]
        [MaxLength(100, ErrorMessage="The length of the title should be less than 100 characters!")]
        public string Title { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
