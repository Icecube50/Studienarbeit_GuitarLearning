using GuitarLearning_TabulatorGenerator.Storage.GuitarStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_TabulatorGenerator.MusicalNotes
{
    public static class MusicalCalculator
    {
        public static string GetNameFromPosition(GuitarStringType guitarString, int position)
        {
            TabelInit();
            int start = GetStartPosition(guitarString);
            int lookup = (start + position) % 12;
            return NoteNames[lookup];
        }

        private static int GetStartPosition(GuitarStringType guitarString)
        {
            if (guitarString == GuitarStringType.E) return 4;
            if (guitarString == GuitarStringType.D) return 2;
            if (guitarString == GuitarStringType.A) return 9;
            if (guitarString == GuitarStringType.G) return 7;
            if (guitarString == GuitarStringType.B) return 11;
            if (guitarString == GuitarStringType.e) return 4;
            return -1;
        }

        private static Dictionary<int, string> NoteNames { get; set; }
        private static void TabelInit()
        {
            NoteNames = new Dictionary<int, string>();
            NoteNames.Add(0, "C");
            NoteNames.Add(1, "C#");
            NoteNames.Add(2, "D");
            NoteNames.Add(3, "D#");
            NoteNames.Add(4, "E");
            NoteNames.Add(5, "F");
            NoteNames.Add(6, "F#");
            NoteNames.Add(7, "G");
            NoteNames.Add(8, "G#");
            NoteNames.Add(9, "A");
            NoteNames.Add(10, "A#");
            NoteNames.Add(11, "B");
        }
    }
}
