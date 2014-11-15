namespace PeugeotForum.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Globalization;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    using PeugeotForum.Data;
    using PeugeotForum.Models.Kendo;

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

        [OutputCache(Duration = 120)]
        public ActionResult Users_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.data.Users.All()
                .OrderByDescending(u => u.DateRegistered).Select(ApplicationUserGridViewModel.FromUser);
            var json = Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            return json;
        }
    } 
}