namespace PeugeotForum.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;

    using PeugeotForum.Models;
    using PeugeotForum.Data.Migrations;

    public class PeugeotForumDbContext : IdentityDbContext<ApplicationUser>
    {
        public PeugeotForumDbContext()
            : base("PeugeotForumConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PeugeotForumDbContext, Configuration>());
        }

        public static PeugeotForumDbContext Create()
        {
            return new PeugeotForumDbContext();
        }

        public IDbSet<Note> Notes { get; set; }

        public IDbSet<Topic> Topics { get; set; }

        public IDbSet<Post> Posts { get; set; }

        public IDbSet<Category> Categories { get; set; }
        
    }
}
