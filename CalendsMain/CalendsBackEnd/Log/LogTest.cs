using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;

namespace CalendsBackEnd.Log
{
    [TestFixture]
    class LogTest
    {
        private LogService logService;
        private String fileName;
        private String logDate;

        private String lineToCheck1;
        private String lineToCheck2;
        private String lineToCheck3;

        public LogTest() { }

        [OneTimeSetUp]
        public void setUp()
        {
            DateTime date = DateTime.Now;
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            String todaysDate = "";
            if(day<10) {
                todaysDate += year + "" + month + "0" + day;
            }
            else
            {
                todaysDate += year + "" + month + "" + day;
            }

            fileName = "CalendsLog_"+todaysDate+".txt";

            DateTime dateTime = DateTime.Now;
            logDate = dateTime.ToString();
            //name of file usually made is CleanSweepLog_yyyymmdd.txt

            if(File.Exists(fileName)){
                File.Delete(fileName);
                if(File.Exists(fileName)){
                    //If delete failed and file is there, end JUNIT test since
                    //it will fail
                    Console.WriteLine("Old file cannot be deleted");
                    Environment.Exit(1);
                }
            }

            logService = LogService.getInstance();

            lineToCheck1 = logDate + ": " + "Hello Line 1!";
            logService.writeLineToLog("Hello Line 1!");
            lineToCheck2 = logDate + ": " + "Hello Line 2!";
            logService.writeLineToLog("Hello Line 2!");
            lineToCheck3 = logDate + ": " + "Hello Line 3!";
            logService.writeLineToLog("Hello Line 3!");
        }

    [OneTimeTearDown]
        public void tearDown()
        {
            //Close log
            if(logService!=null){
                logService.closeLog();
            }
        }


        [Test]
        public void writeLineToLog()
        {

            //Lines to check and see if file is equal to
            String linesToCheck = lineToCheck1+lineToCheck2+lineToCheck3;
            //Check with written method below if the file has these lines
            Assert.AreEqual(linesToCheck,readLineFromFile(0));

        }


        [Test]
        public void closeLog()
        {
            logService.closeLog();
            //Close file
            StreamReader log;
            //Default to true if it closed
            Boolean fileClosed=true;

            //Try to open file, if it can open than the file must have closed earlier
            //If it fails and runs into catch, file did not close properly
            try {
                log = new StreamReader(fileName);
                log.Close();
                log.Dispose();
            }
            catch(IOException ioe){
                Console.WriteLine("Issue Reading Log Object/File");
                Console.WriteLine(ioe.Message);
                fileClosed = false;
            }

            Assert.True(fileClosed);

        }

        [Test]
        public void readWholeLog()
        {
            //Checks lines written in file previously by using readWholeLog method
            String linesToCheck = lineToCheck1+lineToCheck2+lineToCheck3;
            Assert.AreEqual(linesToCheck,logService.readWholeLog(""));
        }

        [Test]
        public void readLogFromSpecifiedLine()
        {
            //Checks lines written in file previously by using readLogFromSpecifiedLine method
            Assert.AreEqual(lineToCheck3,logService.readLogFromSpecifiedLine("",4));

        }

        public String readLineFromFile(int lineNumber)
        {
            //Use buffered reader to read in lines just written to file
            StreamReader log;

            String line;
                int currentLineNumber = 0;

            //Save all the lines in a string object
            String totalOutput = "";

                //Read in recently written to file
            try {
                log = new StreamReader(fileName);

                while ((line = log.ReadLine()) != null)
                {
                    currentLineNumber++;
                    if (currentLineNumber >= lineNumber)
                    {
                        Console.WriteLine(line);
                        totalOutput += line;
                    }
                }
                if (currentLineNumber < lineNumber)
                {
                   Console.WriteLine("File is not " + lineNumber + " lines long");
                }
                log.Close();
                log.Dispose();
            }
            catch(IOException ioe){
                Console.WriteLine("Issue Reading Log Object/File");
                Console.WriteLine(ioe.Message);
            }

                return totalOutput;
        }

    }
}
