namespace PeugeotForum.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using PeugeotForum.Data;

    public class BaseController : Controller
    {
        protected IPeugeotForumData data;

        public BaseController(IPeugeotForumData data)
        {
            this.data = data;
        }
    }
}