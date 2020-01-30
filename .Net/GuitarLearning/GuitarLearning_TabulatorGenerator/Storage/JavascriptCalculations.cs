using GuitarLearning_TabulatorGenerator.MusicalNotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_TabulatorGenerator.Storage
{
    public static class JavascriptCalculations
    {
        public static FrameOptions GetIntervallTime(int optionNumber)
        {
            double timeDuration = MusicalStorage.SongDurationInMS();

            double physDuration = MusicalStorage.CalculateSongDuration();

            double delta = physDuration / timeDuration;

            if(optionNumber == 0)
            {
                double interval = 1.0 / delta;
                var options = new FrameOptions();
                options.frameInterval = interval;
                options.numberOfPixels = 1;
                return options;
            }
            else if(optionNumber == 1)
            {
                var options = new FrameOptions();
                options.frameInterval = 1;
                options.numberOfPixels = delta;
                return options;
            }
            else
            {
                throw new Exception("invalid optionNumber");
            }
        }

        public static double GetBPS()
        {
            //Get time per beat
            double bpm = SongOptions.BpmValue;
            double bps = 60 / bpm;
            bps = bps * 1000; //Convert to ms
            return bps;
        }

        public static int GetAnimationStoppingPoint()
        {
            double length = MusicalStorage.CalculateSongDuration() + StyleOptions.TabInfoSize;
            length = length - StyleOptions.TabInfoSize;
            length = length - (StyleOptions.PreTabSize / 2);
            double correctedLength = Math.Ceiling(length);
            double negatedLength = -correctedLength;
            return Convert.ToInt32(negatedLength);
        }
    }

    public struct FrameOptions
    {
        public double numberOfPixels;
        public double frameInterval;
    }
}
