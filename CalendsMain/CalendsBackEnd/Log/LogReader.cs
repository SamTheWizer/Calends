using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendsBackEnd.Log
{
    /// <summary>
    /// LogWriter interface defines minimal methods needed to read from a log
    /// </summary>
    /// <seealso cref="CalendsLogReaderImpl"/>
    interface LogReader
    {
        String readWholeFile();
        String readFromSpecifiedLine(int lineNumber);
        void closeLog();
        void disposeLog();
    }
}
