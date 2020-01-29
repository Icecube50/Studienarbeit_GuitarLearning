using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarLearning_Mobile.UtilityClasses
{
    /// <summary>
    /// Implements methods used for time based calculations.
    /// </summary>
    public static class TimeHelper
    {
        /// <summary>
        /// Stores the point in time the recording / the song started.
        /// </summary>
        /// <value>Gets/Sets the StartTime DateTime filed.</value>
        private static DateTime StartTime { get; set; }
        /// <summary>
        /// Sets the <see cref="StartTime"/> to now.
        /// </summary>
        public static void SetStartTime()
        {
            StartTime = DateTime.Now;
        }
        /// <summary>
        /// Calculates the time that has elapsed since the <see cref="StartTime"/>.
        /// </summary>
        /// <returns>Number of milliseconds since the start as double.</returns>
        public static double GetElapsedTime()
        {
            DateTime now = DateTime.Now;
            var elapsed = now.Subtract(StartTime);
            return elapsed.TotalMilliseconds;
        }
    }
}
