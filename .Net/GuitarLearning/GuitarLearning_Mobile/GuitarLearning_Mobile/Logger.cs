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
        /// Contains the file name for all generic log entries.
        /// </summary>
        private const string GENERIC_LOG = "LogAndroid.txt";
        /// <summary>
        /// Contains the file name for all API log entries.
        /// </summary>
        private const string API_LOG = "ApiLog.txt";
        /// <summary>
        /// Contains the file name for all recorder log entries.
        /// </summary>
        private const string RECORDER_LOG = "RecorderLog.txt";
        /// <summary>
        /// Contains the file name for all analyser log entries.
        /// </summary>
        private const string ANALYSER_LOG = "AnalyserLog.txt";
        /// <summary>
        /// Contains the name of the directory where all log files are saved
        /// </summary>
        private const string LOG_DIRECTORY = "GuitarLearning";

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
                    androidPath = Path.Combine(androidPath, LOG_DIRECTORY);
                    if (!Directory.Exists(androidPath))
                        Directory.CreateDirectory(androidPath);
                    logFilePath = Path.Combine(androidPath, GENERIC_LOG);
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
                    androidPath = Path.Combine(androidPath, LOG_DIRECTORY);
                    if (!Directory.Exists(androidPath))
                        Directory.CreateDirectory(androidPath);
                    logFilePath = Path.Combine(androidPath, RECORDER_LOG);
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
        /// The File is written to <code>Android.OS.Environment.GetExternalStoragePublicDirectory(string.Empty).AbsolutePath + "GuitarLearning\ApiLog.txt"</code>
        /// </summary>
        /// <param name="logMessage">Message that shall be written into the log file.</param>
        public static void APILog(string logMessage)
        {
            try
            {
                string androidPath = Android.OS.Environment.GetExternalStoragePublicDirectory(string.Empty).AbsolutePath;
                if (Directory.Exists(androidPath))
                {
                    androidPath = Path.Combine(androidPath, LOG_DIRECTORY);
                    if (!Directory.Exists(androidPath))
                        Directory.CreateDirectory(androidPath);
                    logFilePath = Path.Combine(androidPath, API_LOG);
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
                    androidPath = Path.Combine(androidPath, LOG_DIRECTORY);
                    if (!Directory.Exists(androidPath))
                        Directory.CreateDirectory(androidPath);
                    logFilePath = Path.Combine(androidPath, ANALYSER_LOG);
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
        /// Delete all log files
        /// </summary>
        public static void CleanLogs()
        {
            string androidPath = Android.OS.Environment.GetExternalStoragePublicDirectory(string.Empty).AbsolutePath;
            if (Directory.Exists(androidPath))
            {
                androidPath = Path.Combine(androidPath, LOG_DIRECTORY);
                string[] filesToDelete = new string[4];

                filesToDelete[0] = Path.Combine(androidPath, GENERIC_LOG);
                filesToDelete[1] = Path.Combine(androidPath, API_LOG);
                filesToDelete[2] = Path.Combine(androidPath, ANALYSER_LOG);
                filesToDelete[3] = Path.Combine(androidPath, RECORDER_LOG);

                DeleteAllLogs(filesToDelete);
            }
        }

        /// <summary>
        /// Setter for the string field, <see cref="logFilePath"/>
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
        /// <summary>
        /// Try and delete all files specified in the input parameter.
        /// </summary>
        /// <param name="files">Each entry should contain a path to a file, which should be deleted.</param>
        static void DeleteAllLogs(string[] files)
        {
            lock (_locker)
            {
                foreach(string file in files)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch
                    {
                        //file not found or no access, ignore for now
                    }
                }
            }
        }
    }
}
