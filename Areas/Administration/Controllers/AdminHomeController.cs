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
    using System.Collections.Generic;

    [Authorize(Roles = "Administrator")]
    public class AdminHomeController : BaseController
    {
        private const int PAGE_SIZE = 10;

        public AdminHomeController(IPeugeotForumData data)
            : base(data)
        {

        }

        [HttpGet]
        public ActionResult Index(int? page, bool? sortByUsername, bool? reverse, string roleFilter)
        {
            if (page == null || page < 0)
            {
                page = 0;
                TempData["PageNumber"] = 0;
            }
            else
            {
                TempData["PageNumber"] = page;
            }

            if (sortByUsername == null)
            {
                TempData["sortByUsernameReverse"] = true;
                sortByUsername = false;
            }
            else
            {
                TempData["sortByUsernameReverse"] = reverse;
            }

            var usersList = new List<AccountsListViewModel>();
            if (reverse == true)
            {
                usersList = this.data.Users
               .All()
               .OrderBy(u => u.UserName)
               .Select(AccountsListViewModel.FromApplicationUser)
               .OrderByDescending(r => r.Username)
               .Where(u => string.IsNullOrEmpty(roleFilter) || roleFilter == "All" ? true : u.RoleId == roleFilter)
               .Skip((int)page * (PAGE_SIZE))
               .Take(PAGE_SIZE + 1)
               .ToList();
            }
            else
            {
                usersList = this.data.Users
                .All()
                .OrderBy(u => u.UserName)
                .Select(AccountsListViewModel.FromApplicationUser)
                .Where(u => string.IsNullOrEmpty(roleFilter) || roleFilter == "All" ? true : u.RoleId == roleFilter)
                .OrderBy(r => r.Username)
                .Skip((int)page * (PAGE_SIZE))
                .Take(PAGE_SIZE + 1)
                .ToList();
            }


            usersList.ForEach(m => m.RoleName = this.data.Roles.All()
                .Where(r => r.Id == m.RoleId)
                .FirstOrDefault().Name);

            ViewBag.Roles = this.data.Roles.All().Select(r => new RoleViewModel
            {
                RoleId = r.Id,
                RoleName = r.Name
            }).ToList();

            if (usersList.Count() > PAGE_SIZE)
            {
                usersList = usersList.Take(usersList.Count - 1).ToList();
                TempData["LastPage"] = false;
            }
            else
            {
                TempData["LastPage"] = true;
            }

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