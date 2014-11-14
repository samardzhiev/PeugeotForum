namespace PeugeotForum.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class CreateTopicViewModel
    {
        private const int NO_CHOOSEN_CATEGORY = 99;

        private ICollection<SelectListItem> categories;

        public CreateTopicViewModel()
        {
            this.categories = new HashSet<SelectListItem>();
        }

        [Required]
        [MinLength(10, ErrorMessage = "The length of the title should be at least 10 characters!")]
        [MaxLength(100, ErrorMessage = "The length of the title should be less than 100 characters!")]
        public string Title { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "The length of the content should be at least 10 characters!")]
        [MaxLength(1000, ErrorMessage = "The length of the content should be less then 1000 characters!")]
        public string Content { get; set; }

        [Display(Name="Category")]
        [Required(ErrorMessage="Plase choose category!")]
        [Range(0, NO_CHOOSEN_CATEGORY - 1, ErrorMessage="Please choose category!")]
        public int CategoryId { get; set; }

        public ICollection<SelectListItem> Categories { get; set; }
    }
}