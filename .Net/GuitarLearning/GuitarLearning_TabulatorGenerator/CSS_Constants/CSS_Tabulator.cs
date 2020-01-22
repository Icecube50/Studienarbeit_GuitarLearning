using GuitarLearning_TabulatorGenerator.MusicalNotes;
using GuitarLearning_TabulatorGenerator.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_TabulatorGenerator.CSS_Constants
{
    public static class CSS_Tabulator
    {
        public static string ClassName { get; private set; } = "divTabulator";

        public static string SerializeCSS()
        {
            string css = string.Empty;

            //Opening
            css += "." + ClassName + "{\n";

            //Properties
            css += "position: absolute;\n";
            css += "left: 0px;\n";
            css += "top: " + (StyleOptions.HeaderLength + 1) + "px;\n";
            css += "height: " + StyleOptions.ContentLength + "px;\n";
            css += "width: " + MusicalStorage.CalculateSongDuration() + "px;\n";

            //Closing
            css += "}\n";

            //Generate the css that is needed for the tags inside the tabulator
            css += CSS_MusicalNote.SerializeCSS();
            css += CSS_AllStrings.SerializeCSS();
            css += MusicalStorage.SerializeMelodieToCSS();

            return css;
        }
    }

    public static class CSS_MusicalNote
    {
        public static string ClassName { get; private set; } = "divMusicalNote";

        public static string SerializeCSS()
        {
            string css = string.Empty;

            //Opening
            css += "." + ClassName + "{\n";

            //Properties
            css += "width: auto;\n";
            css += "height: auto;\n";
            css += "background-color: white;\n";

            //Closing
            css += "}\n";

            return css;
        }
    }


}
