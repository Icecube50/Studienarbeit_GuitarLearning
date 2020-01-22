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
    public class MusicalNote_Quarter : MusicalNote
    {
        public override IGuitarString StringOfNote { get; set; }
        public override int NoteValue { get; set; }
        public override string NoteID { get; set; }
        public override bool IsStrokeChange { get; set; }

        public MusicalNote_Quarter(GuitarStringType stringType, int noteValue, string id, bool strokeChange)
        {
            StringOfNote = ClassicalGuitar.EnumToObj(stringType);
            NoteID = "Quarter_" + id;
            NoteValue = noteValue;
            IsStrokeChange = strokeChange;
        }

        public override int CalculateDuration()
        {
            return IsStrokeChange == true ? StyleOptions.SizeOfQuarter + StyleOptions.ExtraSizeAfterStroke : StyleOptions.SizeOfQuarter;
        }

        public override HTML_NoteDiv ToHTML()
        {
            HTML_NoteDiv divNote = new HTML_NoteDiv(CSS_MusicalNote.ClassName, NoteID, NoteValue.ToString());
            return divNote;
        }

        public override string ToCSS(int positionLeft)
        {
            positionLeft = positionLeft + StyleOptions.ExtraSizeAfterStroke; //Stroke has to be positioned after the note-duration but before the extra buffer.
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

        public override HTML_Div StrokeSeperatorToHTML()
        {
            HTML_Div divStroke = new HTML_Div(CSS_MusicalStroke.ClassName, NoteID + "_Stroke");
            return divStroke;
        }

        public override string StrokeSeperatorToCSS(int positionLeft)
        {
            string css = string.Empty;

            //Opening
            css += "#" + NoteID + "_Stroke{\n";

            //Properties
            css += "position: absolute;\n";
            css += "left: " + positionLeft + "px;\n";
            css += "top: " + (ClassicalGuitar.E.CalculateTop(6) - 9) + "px;\n";

            //Closing
            css += "}\n";

            return css;
        }
    }
}
