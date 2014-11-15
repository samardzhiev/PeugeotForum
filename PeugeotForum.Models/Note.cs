namespace PeugeotForum.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Note
    {
        public int NoteId { get; set; }

        [Required]
        [MinLength(3)]
        public string Title { get; set; }

        [Required]
        [MinLength(3)]
        public string Content { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser{ get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
