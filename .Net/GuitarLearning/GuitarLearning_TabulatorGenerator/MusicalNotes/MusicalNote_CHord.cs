using GuitarLearning_TabulatorGenerator.CSS_Constants;
using GuitarLearning_TabulatorGenerator.HTML_Serializing;
using GuitarLearning_TabulatorGenerator.Storage;
using GuitarLearning_TabulatorGenerator.Storage.GuitarStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_TabulatorGenerator.MusicalNotes
{
    public class MusicalNote_Chord : MusicalNote
    {
        public override string NoteID { get; set; } 
        public override int NoteValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); } //Not needed
        public override IGuitarString StringOfNote { get => throw new NotImplementedException(); set => throw new NotImplementedException(); } //Not needed


        public string ChordName { get; set; }
        public Tuple<IGuitarString, int>[] StringValueTuples { get; set; }
        public NoteTypes Duration { get; set; }
        public MusicalNote_Chord(Tuple<GuitarStringType, int>[] tuples, string id, NoteTypes duration, string chordName)
        {
            NoteID = "Chord_" + id;
            Duration = duration;
            ChordName = chordName;

            if (tuples.Length > 6) throw new Exception("Only six tupels are allowed (One for each guitar string)!");
            else
            {
                StringValueTuples = new Tuple<IGuitarString, int>[tuples.Length];
                for(int i = 0; i < StringValueTuples.Length; i++)
                {
                    StringValueTuples[i] = new Tuple<IGuitarString, int>(ClassicalGuitar.EnumToObj(tuples[i].Item1),
                        tuples[i].Item2);
                }
            }
        }

        public override int CalculateDuration()
        {
            if (Duration == NoteTypes.Whole) return StyleOptions.SizeOfQuarter * 4;
            if (Duration == NoteTypes.Half) return StyleOptions.SizeOfQuarter * 2;
            if (Duration == NoteTypes.Quarter) return StyleOptions.SizeOfQuarter;
            if (Duration == NoteTypes.Eighth) return StyleOptions.SizeOfQuarter / 2;
            return 0;
        }

        public override double GetMusicalDuration()
        {
            if (Duration == NoteTypes.Whole) return  4;
            if (Duration == NoteTypes.Half) return 2;
            if (Duration == NoteTypes.Quarter) return 1;
            if (Duration == NoteTypes.Eighth) return 0.5;
            return 0;
        }

        public override string ToCSS(double positionLeft)
        {
            string css = string.Empty;

            int noteNumber = 0;
            foreach (var tupel in StringValueTuples)
            {
                //Opening
                css += "#" + (NoteID + "-" + noteNumber) + "{\n";

                //Properties
                css += "position: absolute;\n";
                css += "top: " + (tupel.Item1.CalculateTop(6) - 10) + "px;\n";
                css += "left: " + positionLeft + "px;\n";

                //Closing
                css += "}\n";
                noteNumber++;
            }

            return css;
        }

        public List<HTML_NoteDiv> ChordToHTML()
        {
            List<HTML_NoteDiv> chord = new List<HTML_NoteDiv>();
            int noteNumber = 0;
            foreach (var tupel in StringValueTuples)
            {
                chord.Add(new HTML_NoteDiv(CSS_MusicalNote.ClassName, (NoteID + "-" + noteNumber), tupel.Item2.ToString()));
                noteNumber++;
            }
            return chord;
        }

        //Not needed ↓
        public override HTML_NoteDiv ToHTML()
        {
            throw new NotImplementedException();
        }
        public override HTML_Div StrokeSeperatorToHTML()
        {
            throw new NotImplementedException();
        }
    }
}
