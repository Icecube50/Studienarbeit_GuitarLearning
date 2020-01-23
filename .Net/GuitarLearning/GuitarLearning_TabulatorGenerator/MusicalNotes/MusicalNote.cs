using GuitarLearning_TabulatorGenerator.HTML_Serializing;
using GuitarLearning_TabulatorGenerator.Storage.GuitarStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_TabulatorGenerator.MusicalNotes
{
    public abstract class MusicalNote
    {
        public abstract IGuitarString StringOfNote { get; set; }
        public abstract int NoteValue { get; set; }
        public abstract string NoteID { get; set; }
        public abstract int CalculateDuration();
        public abstract HTML_NoteDiv ToHTML();
        public abstract string ToCSS(double positionLeft);
        public abstract HTML_Div StrokeSeperatorToHTML();
        public abstract double GetMusicalDuration();
    }

    public enum NoteTypes
    {
        Whole,
        Half,
        Quarter,
        Eighth,
    }
}
