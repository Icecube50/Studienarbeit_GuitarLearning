namespace GuitarLearning_Mobile.DeveloperSupport
{
    /// <summary>
    /// This class contains all flags that enable or disable some developing options.
    /// Sould not be used in a release version of the application, but can be useful while testing.
    /// </summary>
    public static class DevFlags
    {
        /// <summary>
        /// Enable or disable logging
        /// </summary>
        /// <value>The LoggingEnabled property gets/sets the value of the bool filed.</value>
        public static bool LoggingEnabled { get; set; } = false;

        /// <summary>
        /// The Deviation property represents the time deviation in which notes are treated as "correct".
        /// </summary>
        /// <value>The Deviation property gets/sets the value of the int field. The default value is 300</value>
        public static int Deviation { get; set; } = 300;
    }
}
