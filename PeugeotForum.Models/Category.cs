namespace PeugeotForum.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Category
    {
        private ICollection<Topic> topics;

        public Category()
        {
            this.topics = new HashSet<Topic>();
        }

        public int CategoryId { get; set; }

        [Required]
        //[Index(IsUnique=true)]
        public string Name { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }
    }
}
