using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;

namespace CalendsBackEnd.Calendar
{
    /// <summary>
    /// Calendar class contains information necessary to construct a calendar event or block
    /// </summary>
    class CalendsEvent
    {
        public DateTime start;
        public DateTime end;
        public String summary;
        public String description;
        public String timeZone;

        public CalendsEvent()
        {

        }
    }
}
