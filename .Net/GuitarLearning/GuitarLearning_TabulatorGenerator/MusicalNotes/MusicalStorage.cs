using GuitarLearning_TabulatorGenerator.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_TabulatorGenerator.MusicalNotes
{
    public static class MusicalStorage
    {
        public static List<MusicalNote> Melodie { get; private set; } = Melodie = new List<MusicalNote>() { new MusicalNote_Stroke("Stroke_0", 0), new MusicalNote_Stroke("Stroke_00", 0)};

        public static void AddNote(MusicalNote musicalNote)
        {
            Melodie.Add(musicalNote);
        }

        public static void DumpStorage()
        {
            Melodie.Clear();
            AddNote(new MusicalNote_Stroke("Stroke_0", 0));
            AddNote(new MusicalNote_Stroke("Stroke_00", 0));
        }

        public static int CalculateSongDuration()
        {
            int songDuration = (StyleOptions.PreTabSize) - (StyleOptions.ExtraSizeAfterStroke * 2); //One stroke as default, so the tab is always rendered. We also have to adjust to the two Strokes in the beginning
            foreach(MusicalNote musicalNote in Melodie)
            {
                songDuration += musicalNote.CalculateDuration();
            }
            return songDuration;
        }

        public static int SongDurationInMS()
        {
            double interval = JavascriptCalculations.GetBPS();

            double songDuration = (StyleOptions.PreStrokes / 2) * interval;
            foreach (MusicalNote musicalNote in Melodie)
            {
                if (musicalNote is MusicalNote_Stroke) continue;
                songDuration += musicalNote.GetMusicalDuration() * interval;
            }

            double duration = songDuration;
            return Convert.ToInt32(duration);
        }

        public static string SerializeMelodieToCSS()
        {
            string css = string.Empty;
            int position = (StyleOptions.PreTabSize) - (StyleOptions.ExtraSizeAfterStroke * 2) + StyleOptions.TabInfoSize; ; //We had one stroke as default previously, so in conclusion the first note has to start after. We also have to adjust to the two Strokes in the beginning

            foreach (MusicalNote musicalNote in Melodie)
            {
                css += musicalNote.ToCSS(position);
                position += musicalNote.CalculateDuration();
            }

            return css;
        }
    }
}
