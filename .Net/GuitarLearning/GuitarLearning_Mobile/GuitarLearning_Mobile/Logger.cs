using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GuitarLearning_Mobile
{
    public static class Logger
    {
        static readonly object _locker = new object();
        static string logFilePath = string.Empty;

        public static void Log(string logMessage)
        {
            try
            {
                //logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "LogAndroid.txt");
                //Use this for daily log files : "Log" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                WriteToLog(logMessage, logFilePath);
            }
            catch (Exception e)
            {
                //log log-exception somewhere else if required!
            }
        }

        public static void ChangeLogFilePath(string newPath)
        {
            logFilePath = newPath;
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
