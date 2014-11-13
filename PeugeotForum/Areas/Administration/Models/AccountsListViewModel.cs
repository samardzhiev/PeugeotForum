namespace PeugeotForum.Areas.Administration.Models
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using PeugeotForum.Models;
    using System.Diagnostics;
    using PeugeotForum.Data;

    public class AccountsListViewModel
    {
        public static Expression<Func<ApplicationUser, AccountsListViewModel>> FromApplicationUser 
        { 
            get 
            {
                return a => new AccountsListViewModel
                {
                    Username = a.UserName,
                    RoleId = a.Roles.FirstOrDefault().RoleId
                };
                    
            }
        }
        public string Username { get; set; }

        public string RoleId { get; set; }

        public string RoleName { get; set; }
    }
}