using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendsBackEnd.Log
{
    /// <summary>
    /// Log service is the primary way to interact with log objects
    /// </summary>
    /// <remarks>
    /// Forces developer using the log to use the service the way it was intended
    /// </remarks>
    class LogService
    {
        private volatile static LogService LogManager;
        private LogWriter logWriter;
        private String todaysDate;

        /// <summary>
        /// Creates a LogService object and places it in LogManager variable
        /// and returns it so developer can use that object to make method calls
        /// </summary>
        /// <remarks>
        /// Set up to only ever have one instance of LogManager
        /// The program should never need multiple LogManagers and will
        /// return the same one each time it is called 
        /// </remarks>
        /// <seealso cref="LogWriter"/>
        /// <seealso cref="LogReader"/>
        public static LogService getInstance()
        { 
            if (LogManager == null)
            {
              LogManager = new LogService();
            }

            return LogManager;
        }

        //Create a log writer
        private LogService()
        {
            logWriter = new CalendsLogWriterImpl();

            DateTime date = DateTime.Now;
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            todaysDate = "";
            if (day < 10)
            {
                todaysDate += year + "" + month + "0" + day;
            }
            else
            {
                todaysDate += year + "" + month + "" + day;
            }

        }

        /// <summary>
        /// Uses LogWriter object to write to a log
        /// </summary>
        /// <param name="message">String message to be written in the log</param>
        public void writeLineToLog(String message)
        {
            if (logWriter != null)
            {
                logWriter.writeToLog(message);
            }
            else
            {
                logWriter = new CalendsLogWriterImpl();
                logWriter.writeToLog(message);
            }
        }

        /// <summary>
        /// Closes log writer
        /// </summary>
        /// <remarks>
        /// This is done so that an object does not keep the log file opened
        /// and the log writer can access it when it needs to
        /// </remarks>
        public void closeLog()
        {
            if (logWriter != null)
            {
                logWriter.closeLog();
                logWriter.disposeLog();
                logWriter = null;
            }
        }

        //Read from file
        private String readFile(String dateOfLog, int lineNumber)
        {
            String logLines = "";
            if (lineNumber == 0)
            {
                CalendsLogReaderImpl logReader = new CalendsLogReaderImpl(dateOfLog);
                logLines += logReader.readWholeFile();
                logReader.closeLog();
                logReader.disposeLog();
            }
            else
            {
                CalendsLogReaderImpl logReader = new CalendsLogReaderImpl(dateOfLog);
                logLines += logReader.readFromSpecifiedLine(lineNumber);
                logReader.closeLog();
                logReader.disposeLog();
            }
            return logLines;
        }

        /// <summary>
        /// Reads whole log from the date specified
        /// if no date is specified, it will read from today
        /// </summary>
        /// <param name="dateOfLog">Date of log wanting to read from</param>
        /// <returns></returns>
        public String readWholeLog(String dateOfLog)
        {
            String logLines = "";

            if (dateOfLog.Equals(todaysDate) || dateOfLog.Equals(""))
            {
                if (logWriter == null)
                {
                    logLines += readFile(dateOfLog, 0);
                }
                else
                {
                    closeLog();
                    logLines += readFile(dateOfLog, 0);
                }
            }
            else
            {
                logLines += readFile(dateOfLog, 0);
            }
            return logLines;
        }


        /// <summary>
        /// Like <see cref="readWholeLog(string)"/> except will start at a specific line number
        /// </summary>
        /// <param name="dateOfLog"></param>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public String readLogFromSpecifiedLine(String dateOfLog, int lineNumber)
        {
            String logLines = "";

            if (dateOfLog.Equals(todaysDate) || dateOfLog.Equals(""))
            {
                if (logWriter == null)
                {
                    logLines += readFile(dateOfLog, lineNumber);
                }
                else
                {
                    closeLog();
                    logLines += readFile(dateOfLog, lineNumber);
                }
            }
            else
            {
                readFile(dateOfLog, lineNumber);
            }

            return logLines;
        }
    }
}
