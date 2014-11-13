namespace PeugeotForum.Controllers
{
    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Calendar.v3;
    using Google.Apis.Calendar.v3.Data;
    using Google.Apis.Services;
    using PeugeotForum.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;

    public class ExternalSyncController : BaseController
    {
        public ExternalSyncController(IPeugeotForumData data)
            : base(data)
        {

        }

        public ActionResult SyncWithGoogleCalendar()
        {
            try
            {
                UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = "592779274982-sk5rssenac5cnabfhuej9hg0fksoi2lk.apps.googleusercontent.com",
                    ClientSecret = "hyczaUTwU1Kgctj_Q1g6zU2V",

                },
                new[] { CalendarService.Scope.Calendar },
                "usert1",
                CancellationToken.None).Result;

                // Create the service.
                var service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Peugeot Fan Club Forum",
                });

                var calendar = new Google.Apis.Calendar.v3.Data.Calendar()
                {
                    Summary = "My Notes (Peugeot Fan Club)",
                    Description = "Your notes in Peugeot Fan Club Forum"
                };

                var calendarsList = service.CalendarList
                    .List()
                    .Execute()
                    .Items
                    .ToList();

                var calendarId = string.Empty;
                foreach (var item in calendarsList)
                {
                    if (item.Summary == "My Notes (Peugeot Fan Club)")
                    {
                        calendarId = item.Id;
                        service.Calendars.Delete(calendarId).Execute();
                        break;
                    }
                }

                calendarId = service.Calendars.Insert(calendar).Execute().Id;
                var userID = this.User.Identity.GetUserId();
                var myNotes = this.data.Notes
                    .All().
                    Where(n => n.ApplicationUserId == userID)
                    .ToList();

                foreach (var note in myNotes)
                {
                    Event ev = new Event();
                    ev.Summary = note.Title;
                    ev.Description = note.Content;
                    var date = new EventDateTime() { Date = note.CreatedOn.ToString("yyyy-MM-dd") };
                    ev.Start = date;
                    ev.End = date;
                    service.Events.Insert(ev, calendarId).Execute();
                }

            }
            catch (AggregateException a)
            {
                return PartialView("_ErrorSync", "No permission granted!");
            }

            return PartialView("_SuccessSync");
        }
    }
}