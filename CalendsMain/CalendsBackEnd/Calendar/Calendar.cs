using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;

namespace CalendsBackEnd.Calendar
{
    /// <summary>
    /// Calendar class takes advantage of Google's Calendar API
    /// to make calls to edit a users' Google Calendar
    /// </summary>
    class Calendar
    {
        private UserCredential cred;
        string[] scope;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Calendar()
        {
            scope = new string[] { CalendarService.Scope.Calendar };
        }
        /// <summary>
        /// Connects to the Google Calendar
        /// </summary>
        /// <returns>Returns true if successful</returns>
        public Boolean connect()
        {
            using (var calSecrets = new FileStream("calends_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/calends.json");

                try
                {
                    cred = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(calSecrets).Secrets,
                    scope,
                    "calendsUser",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Schedules an event on Google Calendar
        /// </summary>
        /// <param name="cEvent"></param>
        /// <returns>Returns true if it was successful</returns>
        public Boolean scheduleEvent(Event cEvent)
        {
            connect();
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = cred,
            });

            try
            {
                EventsResource.InsertRequest request = service.Events.Insert(cEvent, "primary");
                request.Execute();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Deletes an event on a google calendar
        /// </summary>
        /// <param name="cEvent"></param>
        /// <returns>Returns true if it was successful</returns>
        public Boolean deleteEvent(Event cEvent)
        {
            connect();
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = cred,
            });

            try
            {
                EventsResource.DeleteRequest request = service.Events.Delete("primary", cEvent.Id);
                request.Execute();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Gets events from Google Calendar
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public Events getEvents(DateTime startDate, DateTime endDate)
        {
            connect();
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = cred,
            });

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = startDate;
            request.TimeMax = endDate;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = request.Execute();
            return events;
        }

        /// <summary>
        /// Fetch events from Calendar for a single date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public Events getEvents(DateTime date)
        {
            connect();
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = cred,
            });

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = date;
            request.TimeMax = date.AddDays(1);
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = request.Execute();
            return events;
        }
    }
}



