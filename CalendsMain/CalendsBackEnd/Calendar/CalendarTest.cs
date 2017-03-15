using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace CalendsBackEnd.Calendar
{
    /// <summary>
    /// Class created to test calendar code
    /// </summary>
    [TestFixture]
    class CalendarTest
    {
        Calendar testCal;
        Calendar testCal2;
        Event testEvent;
        /// <summary>
        /// Constructs variables needed to test calendar
        /// </summary>
        [OneTimeSetUp]
        public void setUp()
        {
            testCal = new Calendar();
            testCal2 = new Calendar();
            testEvent = new Event()
            {
                Summary = "Calends Test",
                Location = "",
                Description = "Testing scheduling an event from the Calends application.",
                Start = new EventDateTime()
                {
                    DateTime = DateTime.Now,
                    TimeZone = "America/Denver",
                },
                End = new EventDateTime()
                {
                    DateTime = DateTime.Now.AddHours(1),
                    TimeZone = "America/Denver",
                },
                Recurrence = new String[] { "RRULE:FREQ=DAILY;COUNT=2" },
                Attendees = new EventAttendee[]
                {
                },
                Reminders = new Event.RemindersData()
                {
                    UseDefault = false,
                    Overrides = new EventReminder[]
                    {
                        new EventReminder() { Method = "email", Minutes = 30 },
                    }
                }
            };
        }

        [OneTimeTearDown]
        public void tearDown()
        {

        }
        /// <summary>
        /// Test to make sure calendar can be connected to
        /// </summary>
        [Test]
        public void testConnection()
        {
            Assert.IsTrue(testCal.connect());
        }
        /// <summary>
        /// Test to see if multiple calendars can be connected to at the same time
        /// </summary>
        [Test]
        public void testMultipleConnections()
        {
            Assert.IsTrue(testCal.connect());
            Assert.IsTrue(testCal2.connect());
        }
        /// <summary>
        /// Test to get past events
        /// </summary>
        [Test]
        public void testFetchingPastDateRange()
        {
            Events events = testCal.getEvents(DateTime.Today.AddDays(-2), DateTime.Today);
            //check that number of days is correct
            Assert.AreEqual(2, events.Items.Count);
            //check events in list, that no event occurs later than today
            foreach (Event eventItem in events.Items)
            {
                Assert.AreEqual(-1, DateTime.Compare(Convert.ToDateTime(eventItem.Start.DateTime).Date, DateTime.Today.Date));
            }
        }
        /// <summary>
        /// Test to get current events from today
        /// </summary>
        [Test]
        public void testFetchingPresentDateRange()
        {
            Events events = testCal.getEvents(DateTime.Today);
            //check that the number of days is correct
            Assert.AreEqual(1, events.Items.Count);
            //check that date of all events matches today
            foreach (Event eventItem in events.Items)
            {
                Assert.AreEqual(0, DateTime.Compare(Convert.ToDateTime(eventItem.Start.DateTime).Date, DateTime.Today.Date));
            }
        }
        /// <summary>
        /// Test to get future scheduled events
        /// </summary>
        [Test]
        public void testFetchingFutureDateRange()
        {
            Events events = testCal.getEvents(DateTime.Today.AddDays(1), DateTime.Today.AddDays(3));
            //check that the number of days is correct
            Assert.AreEqual(2, events.Items.Count);
            //check that no dates pulled are before today
            foreach (Event eventItem in events.Items)
            {
                Assert.AreEqual(1, DateTime.Compare(Convert.ToDateTime(eventItem.Start.DateTime).Date, DateTime.Today.Date));
            }
        }
        /// <summary>
        /// Test to create an event
        /// </summary>
        [Test]
        public void testMakingEvent()
        {
            Assert.IsTrue(testCal.scheduleEvent(testEvent));
        }
        /// <summary>
        /// Test to delete an event
        /// </summary>
        [Test]
        public void testDeletingEvent()
        {
            //get event list and check number of events
            Events calEvents = testCal.getEvents(DateTime.Today);
            int numEvents = calEvents.Items.Count;
            //if numEvents < 2 schedule an event, else get a calends event
            Event delEvent = testEvent;
            if (numEvents < 2)
            {
                testCal.scheduleEvent(testEvent);
            }
            foreach (Event calEvent in testCal.getEvents(DateTime.Today).Items)
            {
                if (calEvent.Summary == testEvent.Summary)
                {
                    delEvent = calEvent;
                }
            }
            //delete event
            Assert.IsTrue(testCal.deleteEvent(delEvent));
            //get event list and check that number of events decreased by one
            Assert.AreEqual(numEvents - 1, testCal.getEvents(DateTime.Today).Items.Count);

        }

    }
}
