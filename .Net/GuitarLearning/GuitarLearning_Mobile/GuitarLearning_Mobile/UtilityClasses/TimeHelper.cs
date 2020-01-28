using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarLearning_Mobile.UtilityClasses
{
    public static class TimeHelper
    {
        private static DateTime StartTime { get; set; }
        public static void SetStartTime()
        {
            StartTime = DateTime.Now;
        }
        public static double GetElapsedTime()
        {
            DateTime now = DateTime.Now;
            var elapsed = now.Subtract(StartTime);
            return elapsed.TotalMilliseconds;
        }
    }
}
