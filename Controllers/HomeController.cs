namespace PeugeotForum.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using PeugeotForum.Data;
    using PeugeotForum.Models;
    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    public class HomeController : BaseController
    {
        public HomeController(IPeugeotForumData data)
            : base(data)
        {

        }

        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(Duration=120)]
        public ActionResult Users_Read([DataSourceRequest]DataSourceRequest request)
        {
            return Json(this.data.Users.All()
                .OrderByDescending(u=>u.DateRegistered)
                .ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    } 
}