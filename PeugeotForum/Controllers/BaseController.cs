namespace PeugeotForum.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using PeugeotForum.Data;
    using PeugeotForum.Models;

    public class BaseController : Controller
    {
        protected IPeugeotForumData data;
        protected const int PAGE_SIZE = 10;
        protected const int NO_CHOOSEN_CATEGORY = int.MaxValue;

        public BaseController(IPeugeotForumData data)
        {
            this.data = data;
        }

        [NonAction]
        protected int CalculateLastPage(Topic topic, int countPosts)
        {
            int lastPage = 0;

            if (countPosts % PAGE_SIZE == 0)
            {
                lastPage = (topic.Posts.Count() / PAGE_SIZE) - 1;
            }
            else
            {
                lastPage = topic.Posts.Count() / PAGE_SIZE;
            }

            return lastPage;
        }
    }
}