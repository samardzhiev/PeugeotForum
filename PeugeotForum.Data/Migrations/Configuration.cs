namespace PeugeotForum.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using PeugeotForum.Models;

    public sealed class Configuration : DbMigrationsConfiguration<PeugeotForumDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PeugeotForumDbContext context)
        {
            if (!context.Roles.Any())
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var adminRole = new IdentityRole { Name = "Administrator" };
                var moderatorRole = new IdentityRole { Name = "Moderator" };
                var userRole = new IdentityRole { Name = "User" };
                manager.Create(adminRole);
                manager.Create(moderatorRole);
                manager.Create(userRole);
            }
            if (!context.Users.Any())
            {
                var admin = new ApplicationUser()
                {
                    Email="admin@outlook.com",
                    UserName = "admin@outlook.com",
                };
                var moderator = new ApplicationUser()
                {
                    Email = "moderator@outlook.com",
                    UserName = "moderator@outlook.com",
                };
                var user = new ApplicationUser()
                {
                    Email = "user@outlook.com",
                    UserName = "user@outlook.com",
                };

                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                userManager.Create(admin, "123456");
                userManager.Create(moderator, "123456");
                userManager.Create(user, "123456");
                userManager.AddToRole(admin.Id, "Administrator");
                userManager.AddToRole(moderator.Id, "Moderator");
                userManager.AddToRole(user.Id, "User");
            }
        }
    }
}
