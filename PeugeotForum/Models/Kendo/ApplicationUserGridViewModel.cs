namespace PeugeotForum.Models.Kendo
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;

    using PeugeotForum.Models;

    public class ApplicationUserGridViewModel
    {
        public static Expression<Func<ApplicationUser, ApplicationUserGridViewModel>> FromUser
        {
            get
            {
                return user => new ApplicationUserGridViewModel()
                {
                    Username = user.UserName,
                    RegisteredOn = user.DateRegistered
                };
            }
        }

        [Required]
        public string Username { get; set; }

        [Required]
        public DateTime RegisteredOn { get; set; }
    }
}