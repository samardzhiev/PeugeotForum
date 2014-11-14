namespace PeugeotForum.Controllers
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;

    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Calendar.v3;
    using Google.Apis.Calendar.v3.Data;
    using Google.Apis.Services;

    using PeugeotForum.Data;
    
    public class ExternalSyncController : BaseController
    {
        public ExternalSyncController(IPeugeotForumData data)
            : base(data)
        {

        }

        public ActionResult SyncWithGoogleCalendar()
        {
            const string CLIENT_ID = "592779274982-np57svfv9idd819je2o9pbr4rn2dpg48.apps.googleusercontent.com";
            const string CLIENT_SECRET = "m8WQp5O3B4ifcmEWwCGNqUQ_";

            var userID = this.User.Identity.GetUserId();

            try
            {
                UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = CLIENT_ID,
                    ClientSecret = CLIENT_SECRET,
                },
                new[] { CalendarService.Scope.Calendar },
                userID,
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