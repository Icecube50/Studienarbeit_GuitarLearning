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
        public static int GetIntervallTime()
        {
            //Get time per beat
            double bpm = SongOptions.BpmValue;
            double bps = 60 / bpm;

            //Get size of beat (in pixels)
            int size = (StyleOptions.SizeOfQuarter * 4) + StyleOptions.ExtraSizeAfterStroke;

            //Get time per pixel
            double pixelPerSec = size / bps;
            double pixelPerMSec = pixelPerSec / 1000;

            //Calculate time needed for 1 pixel
            if(pixelPerMSec <= 1)
            {
                double intervalTime = 1 / pixelPerMSec;
                double correctedIntervalTime = Math.Ceiling(intervalTime); //Get a nice number, add a small margin of error
                return Convert.ToInt32(correctedIntervalTime);
            }
            else
            {
                throw new Exception("The choosen bpm is way to high, please choose a maximum value of 200");
            }

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
}
