﻿using GuitarLearning_TabulatorGenerator.CSS_Constants;
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
    public class MusicalNote_Half : MusicalNote
    {
        public override string NoteID { get; set; }
        public override int NoteValue { get; set; }
        public override IGuitarString StringOfNote { get; set; }

        public MusicalNote_Half(GuitarStringType stringType, int noteValue, string id)
        {
            NoteValue = noteValue;
            StringOfNote = ClassicalGuitar.EnumToObj(stringType);
            NoteID = "Half_" + id;
        }

        public override int CalculateDuration()
        {
            return (StyleOptions.SizeOfQuarter * 2);
        }

        public override double GetMusicalDuration()
        {
            return 2;
        }

        public override string ToCSS(double positionLeft)
        {
            string css = string.Empty;

            //Opening
            css += "#" + NoteID + "{\n";

            //Properties
            css += "position: absolute;\n";
            css += "top: " + (StringOfNote.CalculateTop(6) - 10) + "px;\n";
            css += "left: " + positionLeft + "px;\n";

            //Closing
            css += "}\n";

            return css;
        }

        public override HTML_NoteDiv ToHTML()
        {
            HTML_NoteDiv divNote = new HTML_NoteDiv(CSS_MusicalNote.ClassName, NoteID, NoteValue.ToString());
            return divNote;
        }

        public override HTML_Div StrokeSeperatorToHTML()
        {
            throw new NotImplementedException();
        }
    }
}
