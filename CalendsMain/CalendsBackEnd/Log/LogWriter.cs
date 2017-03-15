using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendsBackEnd.Log
{
    /// <summary>
    /// LogWriter interface defines minimal methods needed to write to a log
    /// </summary>
    /// <seealso cref="CalendsLogWriterImpl"/>
    interface LogWriter
    {
        void writeToLog(String message);
        void closeLog();
        void disposeLog();

    }
}
