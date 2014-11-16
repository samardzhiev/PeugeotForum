namespace PeugeotForum.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Security.Claims;
    using Microsoft.AspNet.Identity;
    using System.ComponentModel.DataAnnotations;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.DateRegistered = DateTime.Now;
            this.Posts = new HashSet<Post>();
            this.Topics = new HashSet<Topic>();
            this.Notes = new HashSet<Note>();
            this.Likes = new HashSet<Like>();
        }

        [Display(Name = "Registered On")]
        public DateTime DateRegistered { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Note> Notes { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
