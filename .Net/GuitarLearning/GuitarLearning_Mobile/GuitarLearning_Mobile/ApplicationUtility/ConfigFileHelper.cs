using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GuitarLearning_Mobile.ApplicationUtility
{
    /// <summary>
    /// Providing methods to read/write the config file.
    /// </summary>
    public static class ConfigFileHelper
    {
        /// <summary>
        /// Address the API can be found at.
        /// </summary>
        /// <value>Gets the ConfigApiAddress string field.</value>
        public static string ConfigApiAddress { get; private set; } = "https://guitarlearningapi.azurewebsites.net/api/Essentia";
        private static string FilePath { get; set; }
        private const string CONFIG_DIRECTORY_NAME = "AppConfig";
        private const string CONFIG_FILE_NAME = "Config.txt";

        /// <summary>
        /// Initialises all directories and writes the default config file if there isn't one already.
        /// </summary>
        public static void InitConfigFile()
        {
            try
            {
                string androidPath = Android.OS.Environment.GetExternalStoragePublicDirectory(string.Empty).AbsolutePath;
                if (Directory.Exists(androidPath))
                {
                    androidPath = Path.Combine(androidPath, Logger.LOG_DIRECTORY);
                    if (!Directory.Exists(androidPath))
                        Directory.CreateDirectory(androidPath);

                    string configPath = Path.Combine(androidPath, CONFIG_DIRECTORY_NAME);
                    if (!Directory.Exists(configPath))
                        Directory.CreateDirectory(configPath);

                    string configFilePath = Path.Combine(configPath, CONFIG_FILE_NAME);
                    if (!File.Exists(configFilePath))
                    {
                        File.WriteAllText(configFilePath, ConfigApiAddress);
                    }
                    else
                    {
                        string address = File.ReadAllText(configFilePath);
                        ConfigApiAddress = address;
                    }
                    FilePath = configFilePath;
                }
            }
            catch (Exception e)
            {
                Logger.Log("Config: " + e.Message);
            }
        }

        /// <summary>
        /// Update the <see cref="ConfigApiAddress"/> with a new value and write it into the config file.
        /// </summary>
        /// <param name="address">new server address</param>
        public static void NewApiAddressIs(string address)
        {
            ConfigApiAddress = address;
            UpdateConfigFile();
        }

        private static void UpdateConfigFile()
        {
            try
            {
                File.WriteAllText(FilePath, ConfigApiAddress);
            }
            catch (Exception e)
            {
                Logger.Log("Config: " + e.Message);
            }
        }
    }
}
