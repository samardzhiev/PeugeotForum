namespace PeugeotForum.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using PeugeotForum.Data;

    public class HomeController : BaseController
    {
        public HomeController(IPeugeotForumData data)
            :base(data)
        {

        }

        public ActionResult Index()
        {
            return View();
        }
    }
}