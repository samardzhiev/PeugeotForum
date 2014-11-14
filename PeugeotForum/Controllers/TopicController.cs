namespace PeugeotForum.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;

    using PeugeotForum.Data;
    using PeugeotForum.Models;

    [Authorize]
    public class TopicController : BaseController
    {
        private const int NO_CHOOSEN_CATEGORY = 99;
        private const int PAGE_SIZE = 2;

        public TopicController(IPeugeotForumData data)
            :base(data)
        {

        }

        public ActionResult Index()
        {
            var model = new CreateTopicViewModel();
            this.AddCategories(model);

            return View(model);
        }

        [ValidateAntiForgeryToken]
        public ActionResult CreateTopic(CreateTopicViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Error");
            }

            var userId = this.User.Identity.GetUserId();

            var newTopic = new Topic()
            {
                ApplicationUserId = userId,
                CategoryId = model.CategoryId,
                Title = model.Title,
            };
            this.data.Topics.Add(newTopic);
            this.data.SaveChanges();

            var topicId = newTopic.TopicId;

            var firstPost = new Post()
            {
                ApplicationUserId = userId,
                Content = model.Content,
                TopicId = topicId,
                CreatedOn = DateTime.Now
            };

            this.data.Posts.Add(firstPost);
            this.data.SaveChanges();

            return RedirectToAction("ViewTopic", new { topicId = topicId, page = 0 });
        }

        public ActionResult ViewTopic(int topicId, int page)
        {
            var topic = this.data.Topics.Find(topicId);

            if (topic == null)
            {
                return RedirectToAction("Index", "Error");
            }

            var model = new TopicViewModel() 
            { 
                Category = topic.Category.Name,
                Posts = topic.Posts
                    .OrderByDescending(p=>p.CreatedOn)
                    .Skip(page * PAGE_SIZE)
                    .Take(PAGE_SIZE)
                    .ToList(),
                Title = topic.Title
            };

            return View(model);
        }

        [NonAction]
        private void AddCategories(CreateTopicViewModel model)
        {
            var categoriesList = this.data.Categories.All().ToList();
            model.Categories = categoriesList.Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.CategoryId.ToString()
            })
            .ToList();

            model.Categories.Add(new SelectListItem()
            {
                Text = "------ Choose Category ------",
                Value = NO_CHOOSEN_CATEGORY.ToString()
            });

            model.Categories = model.Categories.OrderBy(m => m.Text).ToList();
        }
    }
}