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

            var post = new Post
            {
                ApplicationUserId = this.User.Identity.GetUserId(),
                Content = model.PostContent,
                TopicId = model.TopicId,
            };

            this.data.Posts.Add(post);
            this.data.SaveChanges();
            var countPosts = topic.Posts.Count();

            var lastPage = base.CalculateLastPage(topic, countPosts);

            return RedirectToAction("ViewTopic", "Topic", new { topicId = model.TopicId, page = lastPage });
        }

        public ActionResult Like(int postId)
        {
            var post = this.data.Posts.Find(postId);

            if (post == null)
            {
                return RedirectToAction("Index", "Error");
            }

            var userId = this.User.Identity.GetUserId();

            if (!post.IsAbleToLike(userId))
            {
                return PartialView("_LikePartial", post);
            }

            post.Likes.Add(new Like() { ApplicationUserId = userId, PostId = post.PostId });
            this.data.SaveChanges();

            return PartialView("_LikePartial", post);
        }
    }
}