namespace PeugeotForum.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class AddNewPostViewModel
    {
        public int TopicId { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "The length of the content should be at least 10 characters!")]
        [MaxLength(1000, ErrorMessage = "The length of the content should be less then 1000 characters!")]
        public string PostContent { get; set; }
    }
}