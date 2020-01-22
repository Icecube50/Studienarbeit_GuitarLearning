using GuitarLearning_TabulatorGenerator.MusicalNotes;
using GuitarLearning_TabulatorGenerator.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_TabulatorGenerator.CSS_Constants
{
    public static class CSS_RythmInfo
    {
        public static string SerializeCSS()
        {
            string css = string.Empty;

            css += CSS_TabulatorOverlay.SerializeCSS();
            css += CSS_TabulatorInfo.SerializeCSS();
            css += CSS_TabulatorChords.SerializeCSS();
            css += CSS_UpperRythm.SerializeCSS();
            css += CSS_RythmSeperator.SerializeCSS();
            css += CSS_LowerRythm.SerializeCSS();

            return css;
        }
    }

    public static class CSS_UpperRythm
    {
        public static string ClassName { get; private set; } = "divUpperRythm";

        public static string SerializeCSS()
        {
            string css = string.Empty;

            //Opening
            css += "." + ClassName + "{\n";

            //Properties
            css += "position: absolute;\n";
            css += "left: 5px;\n";
            css += "top: " + 1 + "px;\n";
            css += "font-size: " + StyleOptions.TabInfoTextSize + "px;\n";

            //Closings
            css += "}\n";

            return css;
        }
    }

    public static class CSS_LowerRythm
    {
        public static string ClassName { get; private set; } = "divLowerRythm";

        public static string SerializeCSS()
        {
            string css = string.Empty;

            //Opening
            css += "." + ClassName + "{\n";

            //Properties
            css += "position: absolute;\n";
            css += "left: 5px;\n";
            css += "top: " + (1 + StyleOptions.TabInfoTextSize + 2) + "px;\n"; // HeaderLength + 1 => StartPosition, +2 => Small gap between Seperator and Text (x2)
            css += "font-size: " + StyleOptions.TabInfoTextSize + "px;\n";

            //Closing
            css += "}\n";

            return css;
        }
    }

    public static class CSS_RythmSeperator
    {
        public static string ClassName { get; private set; } = "divRythmSeperator";

        public static string SerializeCSS()
        {
            string css = string.Empty;

            //Opening
            css += "." + ClassName + "{\n";

            //Properties
            css += "position: absolute;\n";
            css += "left: 5px;\n";
            css += "top: " + (1 + StyleOptions.TabInfoTextSize + 1) + "px;\n"; // HeaderLength + 1 => StartPosition, +1 => Small gap between Seperator and Text (x1)
            css += "height: 0px;\n";
            css += "width: 20px;\n";
            css += "border: solid 1px black;\n";

            //Closing
            css += "}\n";

            return css;
        }
    }

    public static class CSS_TabulatorInfo
    {
        public static string ClassName { get; private set; } = "divTabulatorInfo";

        public static string SerializeCSS()
        {
            string css = string.Empty;

            //Opening
            css += "." + ClassName + "{\n";

            //Properties
            css += "position: absolute;\n";
            css += "top: 0px;\n";
            css += "left: 0px;\n";
            css += "z-index: 2;\n";
            css += "width: " + (StyleOptions.TabInfoSize) + "px;\n";
            css += "height: " + StyleOptions.ContentLength + "px;\n";

            //Closing;
            css += "}\n";

            return css;
        }
    }

    public static class CSS_TabulatorChords
    {
        public static string ClassName { get; private set; } = "divTabulatorChords";

        public static string SerializeCSS()
        {
            string css = string.Empty;

            //Opening
            css += "." + ClassName + "{\n";

            //Properties
            css += "position: absolute;\n";
            css += "left: 0px;\n";
            css += "top: 0px;\n";
            css += "z-index: 0;\n";
            css += "width: " + (MusicalStorage.CalculateSongDuration() + StyleOptions.TabInfoSize) + "px;\n";
            css += "height: " + StyleOptions.ContentLength + "px;\n";

            //Closing
            css += "}\n";

            return css;
        }
    }

    public static class CSS_TabulatorOverlay
    {
        public static string ClassName { get; private set; } = "divTabulatorOverlay";

        public static string SerializeCSS()
        {
            string css = string.Empty;

            //Opening
            css += "." + ClassName + "{\n";

            //Properties
            css += "position: absolute;\n";
            css += "left: 0px;\n";
            css += "top: " + (StyleOptions.HeaderLength - 10) + "px;\n";
            css += "z-index: 1;\n";
            css += "width: " + (StyleOptions.TabInfoSize) + "px;\n";
            css += "height: " + (StyleOptions.ContentLength + 20)+ "px;\n";
            css += "background-color: white;\n";

            //Closing
            css += "}\n";

            return css;
        }
    }
}
