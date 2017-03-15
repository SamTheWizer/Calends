using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CalendsBackEnd.Log
{
    /// <summary>
    /// Log Reader Impl class actually implements the ability to read from a log
    /// </summary>
    class CalendsLogReaderImpl : LogReader
    {
        /// <summary>
        /// StreamReader object that opens and writes to file
        /// </summary>
        /// <remarks>
        /// Filename will be CalendsLog_YYYYMMDD.txt
        /// </remarks>
        private StreamReader log;

        /// <summary>
        /// Constructor creates StreamReader log object
        /// </summary>
        /// <remarks>
        /// If date input is given, it will open file with prepended date
        /// If it is not given and is null or an empty string, it will open the log for today
        /// </remarks>
        public CalendsLogReaderImpl(String dateInput)
        {
            String dateOfLog;

            if (dateInput == null || dateInput.Equals(""))
            {
                DateTime date = DateTime.Now;
                int year = date.Year;
                int month = date.Month;
                int day = date.Day;

                dateOfLog = "";
                if (day < 10)
                {
                    dateOfLog += year + "" + month + "0" + day;
                }
                else
                {
                    dateOfLog += year + "" + month + "" + day;
                }
            }
            else
            {
                dateOfLog = dateInput;
            }

            try
            {
                String logName = "CalendsLog_" + dateOfLog + ".txt";
                log = File.OpenText(logName);
            }
            catch (IOException ioe)
            {
                Console.WriteLine("Issue Opening Log File");
                Console.WriteLine(ioe.Message);
            }
        }

        /// <summary>
        /// Reads whole file line by line and returns the string
        /// version of it as well to be stored if needed
        /// </summary>
        /// <returns>Output that is displayed on console</returns>
        public String readWholeFile()
        {
            String line;
            String logLines = "";

            try
            {
                while ((line = log.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    logLines += line;
                }
            }
            catch (IOException ioe)
            {
                Console.WriteLine("Issue Reading Log File");
                Console.WriteLine(ioe.Message);
            }
            return logLines;
        }

        /// <summary>
        /// Reads from a specified line on the file onwards in a line by line fashion
        /// and returns the string version of it as well to be stored if needed
        /// </summary>
        /// <returns>Output that is displayed on console</returns>
        public String readFromSpecifiedLine(int lineNumber)
        {
            String line;
            String logLines = "";
            int currentLineNumber = 0;

            try
            {
                while ((line = log.ReadLine()) != null)
                {
                    currentLineNumber++;
                    if (currentLineNumber >= lineNumber)
                    {
                        Console.WriteLine(line);
                        logLines += line;
                    }
                }
                if (currentLineNumber < lineNumber)
                {
                    Console.WriteLine("File is not " + lineNumber + " lines long");
                }
            }
            catch (IOException ioe)
            {
                Console.WriteLine("Issue Reading Log File");
                Console.WriteLine(ioe.Message);
            }
            return logLines;
        }

        /// <summary>
        /// Closes log file so other parts of application can access it
        /// </summary>
        public void closeLog()
        {
            try
            {
                log.Close();
            }
            catch (IOException ioe)
            {
                Console.WriteLine("Issue Opening Log File");
                Console.WriteLine(ioe.Message);
            }
        }

        /// <summary>
        /// Disposes of object to free space
        /// </summary>
        public void disposeLog()
        {
            try
            {
                log.Dispose();
            }
            catch (IOException ioe)
            {
                Console.WriteLine("Issue Opening Log File");
                Console.WriteLine(ioe.Message);
            }
        }
    }
}
