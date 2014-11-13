namespace PeugeotForum.Data
{
    using System;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;

    using PeugeotForum.Data.Repositories;
    using PeugeotForum.Models;

    public interface IPeugeotForumData
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<IdentityRole> Roles { get; }

        IRepository<Note> Notes { get; }

        int SaveChanges();
    }
}
