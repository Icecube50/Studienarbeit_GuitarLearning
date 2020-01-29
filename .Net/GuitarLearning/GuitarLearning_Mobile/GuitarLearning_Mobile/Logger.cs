using System;
using System.IO;

namespace GuitarLearning_Mobile
{
    /// <summary>
    /// Implements several different thread-safe loggers.
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Mutex for generic logging
        /// </summary>
        static readonly object _locker = new object();
        /// <summary>
        /// Path to the log file, currently not used
        /// </summary>
        static string logFilePath = string.Empty;

        /// <summary>
        /// Generic logging method, the file is safed at <code>Android.OS.Environment.GetExternalStoragePublicDirectory(string.Empty).AbsolutePath + "GuitarLearning\LogAndroid.txt"</code>
        /// </summary>
        /// <param name="logMessage">Message that shall be written into the log file.</param>
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

        /// <summary>
        /// Mutex for logging specific to audio recording.
        /// </summary>
        static readonly object _recordLocker = new object();
        /// <summary>
        /// Logging method that is used for logs specific to the audio recording.
        /// The File is written to <code>Android.OS.Environment.GetExternalStoragePublicDirectory(string.Empty).AbsolutePath + "GuitarLearning\RecorderLog.txt"</code>
        /// </summary>
        /// <param name="logMessage">Message that shall be written into the log file.</param>
        public static void RecordingLog(string logMessage)
        {
            try
            {
                string androidPath = Android.OS.Environment.GetExternalStoragePublicDirectory(string.Empty).AbsolutePath;
                if (Directory.Exists(androidPath))
                {
                    androidPath = Path.Combine(androidPath, "GuitarLearning");
                    if (!Directory.Exists(androidPath))
                        Directory.CreateDirectory(androidPath);
                    logFilePath = Path.Combine(androidPath, "RecorderLog.txt");
                    //Use this for daily log files : "Log" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

                    lock (_recordLocker)
                    {
                        File.AppendAllText(logFilePath,
                        string.Format("Logged on: {1} at: {2}{0}Message: {3}{0}--------------------{0}",
                        Environment.NewLine, DateTime.Now.ToLongDateString(),
                        DateTime.Now.ToLongTimeString(), logMessage));
                    }
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

        /// <summary>
        /// Mutex for logging specific to the API communication.
        /// </summary>
        static readonly object _apiLocker = new object();
        /// <summary>
        /// Logging method that is used for logs specific to the API.
        /// The File is writte to <code>Android.OS.Environment.GetExternalStoragePublicDirectory(string.Empty).AbsolutePath + "GuitarLearning\ApiLog.txt"</code>
        /// </summary>
        /// <param name="logMessage">Message that shall be written into the log file.</param>
        public static void APILog(string logMessage)
        {
            try
            {
                string androidPath = Android.OS.Environment.GetExternalStoragePublicDirectory(string.Empty).AbsolutePath;
                if (Directory.Exists(androidPath))
                {
                    androidPath = Path.Combine(androidPath, "GuitarLearning");
                    if (!Directory.Exists(androidPath))
                        Directory.CreateDirectory(androidPath);
                    logFilePath = Path.Combine(androidPath, "ApiLog.txt");
                    //Use this for daily log files : "Log" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

                    lock (_apiLocker)
                    {
                        File.AppendAllText(logFilePath,
                        string.Format("Logged on: {1} at: {2}{0}Message: {3}{0}--------------------{0}",
                        Environment.NewLine, DateTime.Now.ToLongDateString(),
                        DateTime.Now.ToLongTimeString(), logMessage));
                    }
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


        /// <summary>
        /// Mutex for logging specific to the data analysing.
        /// </summary>
        static readonly object _analyzerLocker = new object();
        /// <summary>
        /// Logging method that is used for logs specific to the data analysing.
        /// The file is written to <code>Android.OS.Environment.GetExternalStoragePublicDirectory(string.Empty).AbsolutePath + "GuitarLearning\AnalyzerLog.txt"</code>
        /// </summary>
        /// <param name="logMessage">Message that shall be written into the log file.</param>
        public static void AnalyzerLog(string logMessage)
        {
            try
            {
                string androidPath = Android.OS.Environment.GetExternalStoragePublicDirectory(string.Empty).AbsolutePath;
                if (Directory.Exists(androidPath))
                {
                    androidPath = Path.Combine(androidPath, "GuitarLearning");
                    if (!Directory.Exists(androidPath))
                        Directory.CreateDirectory(androidPath);
                    logFilePath = Path.Combine(androidPath, "AnalyzerLog.txt");
                    //Use this for daily log files : "Log" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

                    lock (_analyzerLocker)
                    {
                        File.AppendAllText(logFilePath,
                        string.Format("Logged on: {1} at: {2}{0}Message: {3}{0}--------------------{0}",
                        Environment.NewLine, DateTime.Now.ToLongDateString(),
                        DateTime.Now.ToLongTimeString(), logMessage));
                    }
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

        /// <summary>
        /// Setter for the string field, <see cref="logFilePath"/>/>
        /// </summary>
        /// <param name="newPath">New file path</param>
        public static void ChangeLogFilePath(string newPath)
        {
            logFilePath = newPath;
        }

        /// <summary>
        /// Method that is used by <see cref="Log(string)"/> to write the log file./>
        /// </summary>
        /// <param name="logMessage">Message that shall be written into the log file.</param>
        /// <param name="logFilePath">Path to the file.</param>
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
