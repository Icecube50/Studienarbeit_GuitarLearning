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
    public class MusicalNote_Stroke : MusicalNote
    {
        public override string NoteID { get; set; } 
        public override int NoteValue { get; set; } //Not needed
        public override IGuitarString StringOfNote { get; set; } //Not needed


        public double Overlap { get; set; }
        public MusicalNote_Stroke(string id, double overlap)
        {
            Overlap = overlap;
            NoteID = "Stroke_" + id;
        }

        public override int CalculateDuration()
        {
            return StyleOptions.ExtraSizeAfterStroke;
        }

        public override HTML_NoteDiv ToHTML()
        {
            throw new NotImplementedException();
        }

        public override HTML_Div StrokeSeperatorToHTML()
        {
            HTML_Div divStroke = new HTML_Div(CSS_MusicalStroke.ClassName, NoteID);
            return divStroke;
        }

        public override string ToCSS(double positionLeft)
        {
            positionLeft = positionLeft - (StyleOptions.SizeOfQuarter * Overlap);

            string css = string.Empty;

            //Opening
            css += "#" + NoteID + "{\n";

            //Properties
            css += "position: absolute;\n";
            css += "left: " + positionLeft + "px;\n";
            css += "top: " + (ClassicalGuitar.HighE.CalculateTop(6) - 9) + "px;\n";

            //Closing
            css += "}\n";

            return css;
        }
    }
}
