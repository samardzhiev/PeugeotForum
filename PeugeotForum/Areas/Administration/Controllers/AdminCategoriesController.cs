namespace PeugeotForum.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections.Generic;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    using PeugeotForum.Controllers;
    using PeugeotForum.Data;
    using PeugeotForum.Models;
    using PeugeotForum.Areas.Administration.Models;
    

    [Authorize(Roles = "Administrator")]
    public class AdminCategoriesController : BaseController
    {
        public AdminCategoriesController(IPeugeotForumData data)
            :base(data)
        {

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCategories([DataSourceRequest]DataSourceRequest request)
        {
            var categories = this.data.Categories.All().ToList().Select(c => new CategoryViewModel()
            {
                Name = c.Name
            });

            DataSourceResult result = categories.ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult AddCategory(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Validation failed. Please try again.");
                return View("Index");
            }

            bool isExistingCategory = this.data.Categories.All().Any(c => c.Name == model.Name);

            if (isExistingCategory)
            {
                ModelState.AddModelError("Error", "Validation failed. The category alrady exists!");
                return View("Index");
            }

            this.data.Categories.Add(new Category() { Name = model.Name });
            this.data.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}