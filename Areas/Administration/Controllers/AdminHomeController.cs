namespace PeugeotForum.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using PeugeotForum.Controllers;
    using PeugeotForum.Data;
    using PeugeotForum.Areas.Administration.Models;
    using PeugeotForum.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using System.Diagnostics;

    [Authorize(Roles = "Administrator")]
    public class AdminHomeController : BaseController
    {
        public AdminHomeController(IPeugeotForumData data)
            : base(data)
        {

        }

        [HttpGet]
        public ActionResult Index()
        {
            var usersList = this.data.Users
                .All()
                .Select(AccountsListViewModel.FromApplicationUser)
                .ToList();
            usersList.ForEach(m => m.RoleName = this.data.Roles.All()
                .Where(r => r.Id == m.RoleId)
                .FirstOrDefault().Name);
            ViewBag.Roles = this.data.Roles.All().Select(r => new RoleViewModel
            {
                RoleId = r.Id,
                RoleName = r.Name
            }).ToList();

            return View(usersList);
        }

        public ActionResult ChangeUserRole(string roleId, string username)
        {
            ChangeRole(roleId, username);
            data.SaveChanges();
            var model = this.data.Users.All().Where(u => u.UserName == username)
                .Select(AccountsListViewModel.FromApplicationUser).FirstOrDefault();
            ViewBag.Roles = this.data.Roles.All().Select(r => new RoleViewModel
            {
                RoleId = r.Id,
                RoleName = r.Name
            }).ToList();
            return this.PartialView("_UserAndRole", model);
        }

        [NonAction]
        public void ChangeRole(string roleId, string username)
        {
            var context = new PeugeotForumDbContext();
            var userId = data.Users.All().Where(u => u.UserName == username).FirstOrDefault().Id;
            var roleName = data.Roles.All().Where(r => r.Id == roleId).FirstOrDefault().Name;
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            userManager.RemoveFromRole(userId, "Administrator");
            userManager.RemoveFromRole(userId, "User");
            userManager.RemoveFromRole(userId, "Moderator");
            userManager.AddToRole(userId, roleName);
        }
    }
}