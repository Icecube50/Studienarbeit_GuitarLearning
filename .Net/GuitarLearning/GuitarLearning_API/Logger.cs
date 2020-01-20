using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GuitarLearning_API
{
    public static class Logger
    {
        static readonly object _locker = new object();

        public static void Log(string logMessage)
        {
            try
            {
                string dic = Directory.GetCurrentDirectory();
                var logFilePath = Path.Combine(dic, "LogServer.txt");
                //Use this for daily log files : "Log" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                WriteToLog(logMessage, logFilePath);
            }
            catch (Exception e)
            {
                //log log-exception somewhere else if required!
            }
        }

        static void WriteToLog(string logMessage, string logFilePath)
        {
            lock (_locker)
            {
                File.AppendAllText(logFilePath,
                        string.Format("Logged on: {1} at: {2}{0}Message: {3}{0}--------------------{0}",
                        Environment.NewLine, DateTime.Now.ToLongDateString(),
                        DateTime.Now.ToLongTimeString(), logMessage));
            }
        }
    }
}
