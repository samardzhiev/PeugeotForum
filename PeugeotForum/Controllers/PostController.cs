namespace PeugeotForum.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;

    using PeugeotForum.Data;
    using PeugeotForum.Models;
    using System.Globalization;

    [Authorize]
    public class PostController : BaseController
    {

        public PostController(IPeugeotForumData data)
            :base(data)
        {

        }

        [ValidateAntiForgeryToken]
        public ActionResult AddNewPost(AddNewPostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Error");
            }
            var topic = this.data.Topics.Find(model.TopicId);
            if (topic == null)
            {
                return RedirectToAction("Index", "Error");
            }

            this.data.Posts.Add(new Post
            {
                ApplicationUserId = this.User.Identity.GetUserId(),
                Content = model.PostContent,
                TopicId = model.TopicId
            });
            this.data.SaveChanges();
            var countPosts = topic.Posts.Count();

            var lastPage = base.CalculateLastPage(topic, countPosts);

            return RedirectToAction("ViewTopic", "Topic", new { topicId = model.TopicId, page = lastPage });
        }
    }
}