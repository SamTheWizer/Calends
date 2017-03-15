using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CalendsBackEnd.Log
{
    /// <summary>
    /// Log Writer Impl class actually does log writer implementation
    /// </summary>
    class CalendsLogWriterImpl : LogWriter
    {
        /// <summary>
        /// StreamWriter object that opens and writes to file
        /// </summary>
        /// <remarks>
        /// Filename will be CalendsLog_YYYYMMDD.txt
        /// </remarks>
        private StreamWriter log;

        /// <summary>
        /// Constructor creates StreamWriter log object
        /// </summary>
        public CalendsLogWriterImpl()
        {
            DateTime date = DateTime.Now;
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            String dateOfLog = "";
            if (day < 10)
            {
                dateOfLog += year + "" + month + "0" + day;
            }
            else
            {
                dateOfLog += year + "" + month + "" + day;
            }

            try
            {
                String logName = "CalendsLog_" + dateOfLog + ".txt";
                log = File.AppendText(logName);
                
            }
            catch (IOException ioe)
            {
                Console.WriteLine("Issue Creating Log Object/File");
                Console.WriteLine(ioe.Message);
            }
        }

        /// <summary>
        /// Writes one line to file
        /// </summary>
        /// <remarks>
        /// Prepends line to be written with the date and time 
        /// </remarks>
        /// <param name="message">Message to be written in file</param>

        //write to the file
        public void writeToLog(String message)
        {
            DateTime date = DateTime.Now;
            String logDate = date.ToString();
            try
            {
                log.WriteLine(logDate + ": " + message + "\r\n");
                log.Flush();
            }
            catch (IOException ioe)
            {
                Console.WriteLine("Issue Writing to Log Object/File");
                Console.WriteLine(ioe.Message);
            }
        }

        /// <summary>
        /// Closes log file so other parts of application can access it
        /// </summary>
        //Close the file
        public void closeLog()
        {
            try
            {
                log.Close();
            }
            catch (IOException ioe)
            {
                Console.WriteLine("Issue Closing Log Object/File");
                Console.WriteLine(ioe.Message);
            }
        }
        /// <summary>
        /// Disposes of object to free space
        /// </summary>
        public void disposeLog()
        {
            log.Dispose();
        }
    }
}
