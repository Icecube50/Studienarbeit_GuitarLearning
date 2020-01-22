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
        public static List<MusicalNote> Melodie { get; private set; } = Melodie = new List<MusicalNote>();

        public static void AddNote(MusicalNote musicalNote)
        {
            Melodie.Add(musicalNote);
        }

        public static void DumpStorage()
        {
            Melodie.Clear();
        }

        public static int CalculateSongDuration()
        {
            int songDuration = StyleOptions.SizeOfQuarter * 4; //One stroke as default, so the tab is always rendered
            foreach(MusicalNote musicalNote in Melodie)
            {
                songDuration += musicalNote.CalculateDuration();
            }
            return songDuration;
        }

        public static string SerializeMelodieToHTML()
        {
            string html = string.Empty;

            foreach(MusicalNote musicalNote in Melodie)
            {
                html += musicalNote.ToHTML();
            }

            return html;
        }

        public static string SerializeMelodieToCSS()
        {
            string css = string.Empty;
            int position = StyleOptions.SizeOfQuarter * 4; //We had one stroke as default previously, so in conclusion the first note has to start after

            foreach (MusicalNote musicalNote in Melodie)
            {
                css += musicalNote.ToCSS(position);
                position += musicalNote.CalculateDuration();
                if (musicalNote.IsStrokeChange) { css += musicalNote.StrokeSeperatorToCSS(position); } //Stroke has to be positioned after the duration of the note
            }

            return css;
        }
    }
}
