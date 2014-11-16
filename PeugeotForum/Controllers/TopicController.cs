namespace PeugeotForum.Controllers
{
    using System;
    using System.Linq;
    using System.Globalization;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using PeugeotForum.Data;
    using PeugeotForum.Models;
    using PeugeotForum.Models.Kendo;

    public class TopicController : BaseController
    {
        public TopicController(IPeugeotForumData data)
            : base(data)
        {

        }

        [Authorize]
        public ActionResult Index()
        {
            var model = new CreateTopicViewModel();
            this.AddCategories(model);

            return View(model);
        }

        [Authorize]
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
            };

            this.data.Posts.Add(firstPost);
            this.data.SaveChanges();

            return RedirectToAction("ViewTopic", new { topicId = topicId, page = 0 });
        }

        [Authorize]
        public ActionResult ViewTopic(int topicId, int page)
        {
            var topic = this.data.Topics.Find(topicId);

            if (page < 0)
            {
                page = 0;
            }

            if (topic == null)
            {
                return RedirectToAction("Index", "Error");
            }
            var countPosts = topic.Posts.Count();
            int lastPage = CalculateLastPage(topic, countPosts);

            if (lastPage == page)
            {
                TempData["LastPage"] = true;
            }
            else
            {
                TempData["LastPage"] = false;
            }

            var model = new TopicViewModel()
            {
                Category = topic.Category.Name,
                Posts = topic.Posts
                    .OrderBy(p => p.CreatedOn)
                    .Skip((int)page * PAGE_SIZE)
                    .Take(PAGE_SIZE)
                    .ToList(),
                Title = topic.Title,
                TopicId = topicId,
                Username = topic.ApplicationUser.UserName
            };

            return View(model);
        }

        [OutputCache(Duration = 120)]
        public ActionResult Topics_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.data.Topics.All()
                .OrderByDescending(u => u.DateCreated).Select(TopicGridViewModel.FromTopic);
            var json = Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            return json;
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