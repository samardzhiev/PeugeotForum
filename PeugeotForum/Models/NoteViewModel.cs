namespace PeugeotForum.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

    public class NoteViewModel
    {
        public static Expression<Func<Note, NoteViewModel>> FromNote
        {
            get
            {
                return note => new NoteViewModel()
                {
                    NoteId = note.NoteId,
                    Title = note.Title,
                    Content = note.Content,
                    CreatedOn = note.CreatedOn
                };
            }
        }

        public int NoteId { get; set; }

        [Required]
        [MinLength(3)]
        public string Title { get; set; }

        [Required]
        [MinLength(3)]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
    }
}