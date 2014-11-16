namespace PeugeotForum.Areas.Administration.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class CategoryViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}