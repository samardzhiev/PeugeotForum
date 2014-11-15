namespace PeugeotForum.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class TopicViewModel
    {
        public TopicViewModel()
        {
            this.Posts = new List<Post>();
        }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public int TopicId { get; set; }

        public IList<Post> Posts { get; set; }

    }
}