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
        public abstract uint NoteValue { get; set; }
        public abstract string NoteID { get; set; }
        public abstract uint CalculateDuration();
        public abstract HTML_NoteDiv ToHTML();
        public abstract string ToCSS(uint positionLeft);
    }

    public enum NoteTypes
    {
        Quarter,
    }
}
