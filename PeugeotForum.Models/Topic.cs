namespace PeugeotForum.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Topic
    {
        private ICollection<Post> posts;

        public Topic()
        {
            this.posts = new HashSet<Post>();
        }

        public int TopicId { get; set; }

        [Required]
        [MinLength(10, ErrorMessage="The length of the title should be at least 10 characters!")]
        [MaxLength(100, ErrorMessage="The length of the title should be less than 100 characters!")]
        public string Title { get; set; }

        public string ApplicationUserId { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
