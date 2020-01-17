using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

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
                string androidPath = Android.OS.Environment.GetExternalStoragePublicDirectory(string.Empty).AbsolutePath;
                if (Directory.Exists(androidPath))
                {
                    androidPath = Path.Combine(androidPath, "GuitarLearning");
                    if (!Directory.Exists(androidPath))
                        Directory.CreateDirectory(androidPath);
                    logFilePath = Path.Combine(androidPath, "LogAndroid.txt");
                    //Use this for daily log files : "Log" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                    WriteToLog(logMessage, logFilePath);
                }
                else
                {
                    throw new Exception("No external storage was found");
                }

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
