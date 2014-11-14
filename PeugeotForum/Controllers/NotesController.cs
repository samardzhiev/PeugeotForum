namespace PeugeotForum.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Globalization;
    using Microsoft.AspNet.Identity;
    
    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    using PeugeotForum.Data;
    using PeugeotForum.Models;
    

    [Authorize]
    public class NotesController : BaseController
    {
        public NotesController(IPeugeotForumData data)
            : base(data)
        {

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReadNotes([DataSourceRequest]DataSourceRequest request)
        {
            var userId = this.User.Identity.GetUserId();
            var results = this.data.Notes
                .All()
                .Where(note => note.ApplicationUserId == userId)
                .Select(NoteViewModel.FromNote);
            return Json(results.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateNote([DataSourceRequest]DataSourceRequest request, NoteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var note = new Note()
                {
                    ApplicationUserId = this.User.Identity.GetUserId(),
                    Title = model.Title,
                    Content = model.Content,
                    NoteId = model.NoteId,
                    CreatedOn = DateTime.Now
                };

                this.data.Notes.Add(note);
                this.data.SaveChanges();
                model.CreatedOn = note.CreatedOn;
                model.NoteId = note.NoteId;
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult UpdateNote([DataSourceRequest]DataSourceRequest request, NoteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var note = new Note()
                {
                    ApplicationUserId = this.User.Identity.GetUserId(),
                    Title = model.Title,
                    Content = model.Content,
                    NoteId = model.NoteId,
                    CreatedOn = model.CreatedOn
                };

                this.data.Notes.Update(note);
                this.data.SaveChanges();
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult DeleteNote([DataSourceRequest]DataSourceRequest request, NoteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var note = new Note()
                {
                    ApplicationUserId = this.User.Identity.GetUserId(),
                    Title = model.Title,
                    Content = model.Content,
                    NoteId = model.NoteId,
                    CreatedOn = model.CreatedOn
                };

                this.data.Notes.Delete(note);
                this.data.SaveChanges();
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        protected override IAsyncResult BeginExecute(System.Web.Routing.RequestContext requestContext, AsyncCallback callback, object state)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            return base.BeginExecute(requestContext, callback, state);
        }
    }
}