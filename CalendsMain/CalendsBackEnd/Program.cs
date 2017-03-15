using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalendsBackEnd.Log;

namespace CalendsBackEnd
{
    class Program
    {
        static void Main(string[] args)
        {
            //Example Log Code
            LogService logService = LogService.getInstance();
            logService.writeLineToLog("Test Line");
            logService.writeLineToLog("Second Test Line");
            logService.writeLineToLog("Third Test Line");
            logService.closeLog();
        }
    }
}
