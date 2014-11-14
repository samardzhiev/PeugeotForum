namespace PeugeotForum.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using PeugeotForum.Data.Repositories;
    using PeugeotForum.Models;

    public class PeugeotForumData : IPeugeotForumData
    {
        private IdentityDbContext<ApplicationUser> context;
        private IDictionary<Type, object> repositories;

        public PeugeotForumData(IdentityDbContext<ApplicationUser> context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<ApplicationUser> Users
        {
            get
            {
                return this.GetRepository<ApplicationUser>();
            }
        }

        public IRepository<IdentityRole> Roles
        {
            get
            {
                return this.GetRepository<IdentityRole>();
            }
        }

        public IRepository<Note> Notes
        {
            get { return this.GetRepository<Note>(); }
        }

        public IRepository<Topic> Topics
        {
            get { return this.GetRepository<Topic>(); }
        }

        public IRepository<Post> Posts
        {
            get { return this.GetRepository<Post>(); }
        }

        public IRepository<Category> Categories
        {
            get { return this.GetRepository<Category>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(EFRepository<T>), context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}
