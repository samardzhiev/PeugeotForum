namespace PeugeotForum.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class TopicViewModel
    {
        private ICollection<Post> posts;

        public TopicViewModel()
        {
            this.posts = new List<Post>();
        }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Category { get; set; }

        public IList<Post> Posts { get; set; }
    }
}