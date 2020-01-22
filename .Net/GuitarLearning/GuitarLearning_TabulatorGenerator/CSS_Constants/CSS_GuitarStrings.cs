using GuitarLearning_TabulatorGenerator.MusicalNotes;
using GuitarLearning_TabulatorGenerator.Storage;
using GuitarLearning_TabulatorGenerator.Storage.GuitarStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_TabulatorGenerator.CSS_Constants
{
    public static class CSS_AllStrings
    {
        public static string SerializeCSS()
        {
            string css = string.Empty;

            css += CSS_StringE.SerializeCSS();
            css += CSS_StringA.SerializeCSS();
            css += CSS_StringD.SerializeCSS();
            css += CSS_StringG.SerializeCSS();
            css += CSS_StringB.SerializeCSS();
            css += CSS_StringHighE.SerializeCSS();

            css += CSS_StringNameE.SerializeCSS();
            css += CSS_StringNameA.SerializeCSS();
            css += CSS_StringNameD.SerializeCSS();
            css += CSS_StringNameG.SerializeCSS();
            css += CSS_StringNameB.SerializeCSS();
            css += CSS_StringNameHighE.SerializeCSS();

            return css;
        }
    }

    public static class CSS_StringE
    {
        public static string ClassName { get; private set; } = "divStringE";

        public static string SerializeCSS()
        {
            string css = string.Empty;

            //Opening
            css += "." + ClassName + "{\n";

            //Properties
            css += "height: 0px;\n";
            css += "width: " + MusicalStorage.CalculateSongDuration() + "px;\n";
            css += "border: solid 1px lightgray;\n";
            css += "position: absolute;\n";
            css += "left: " + StyleOptions.TabInfoSize + "px;\n";
            css += "top: " + ClassicalGuitar.E.CalculateTop(6) + "px;\n";

            //Closing
            css += "}\n";

            return css;
        }
    }

    public static class CSS_StringD
    {
        public static string ClassName { get; private set; } = "divStringD";

        public static string SerializeCSS()
        {
            string css = string.Empty;

            //Opening
            css += "." + ClassName + "{\n";

            //Properties
            css += "height: 0px;\n";
            css += "width: " + MusicalStorage.CalculateSongDuration() + "px;\n";
            css += "border: solid 1px lightgray;\n";
            css += "position: absolute;\n";
            css += "left: " + StyleOptions.TabInfoSize + "px;\n";
            css += "top: " + ClassicalGuitar.D.CalculateTop(6) + "px;\n";

            //Closing
            css += "}\n";

            return css;
        }
    }

    public static class CSS_StringA
    {
        public static string ClassName { get; private set; } = "divStringA";

        public static string SerializeCSS()
        {
            string css = string.Empty;

            //Opening
            css += "." + ClassName + "{\n";

            //Properties
            css += "height: 0px;\n";
            css += "width: " + MusicalStorage.CalculateSongDuration() + "px;\n";
            css += "border: solid 1px lightgray;\n";
            css += "position: absolute;\n";
            css += "left: " + StyleOptions.TabInfoSize + "px;\n";
            css += "top: " + ClassicalGuitar.A.CalculateTop(6) + "px;\n";

            //Closing
            css += "}\n";

            return css;
        }
    }

    public static class CSS_StringG
    {
        public static string ClassName { get; private set; } = "divStringG";

        public static string SerializeCSS()
        {
            string css = string.Empty;

            //Opening
            css += "." + ClassName + "{\n";

            //Properties
            css += "height: 0px;\n";
            css += "width: " + MusicalStorage.CalculateSongDuration() + "px;\n";
            css += "border: solid 1px lightgray;\n";
            css += "position: absolute;\n";
            css += "left: " + StyleOptions.TabInfoSize + "px;\n";
            css += "top: " + ClassicalGuitar.G.CalculateTop(6) + "px;\n";

            //Closing
            css += "}\n";

            return css;
        }
    }

    public static class CSS_StringB
    {
        public static string ClassName { get; private set; } = "divStringB";

        public static string SerializeCSS()
        {
            string css = string.Empty;

            //Opening
            css += "." + ClassName + "{\n";

            //Properties
            css += "height: 0px;\n";
            css += "width: " + MusicalStorage.CalculateSongDuration() + "px;\n";
            css += "border: solid 1px lightgray;\n";
            css += "position: absolute;\n";
            css += "left: " + StyleOptions.TabInfoSize + "px;\n";
            css += "top: " + ClassicalGuitar.B.CalculateTop(6) + "px;\n";

            //Closing
            css += "}\n";

            return css;
        }
    }

    public static class CSS_StringHighE
    {
        public static string ClassName { get; private set; } = "divStringHighE";

        public static string SerializeCSS()
        {
            string css = string.Empty;

            //Opening
            css += "." + ClassName + "{\n";

            //Properties
            css += "height: 0px;\n";
            css += "width: " + MusicalStorage.CalculateSongDuration() + "px;\n";
            css += "border: solid 1px lightgray;\n";
            css += "position: absolute;\n";
            css += "left: " + StyleOptions.TabInfoSize + "px;\n";
            css += "top: " + ClassicalGuitar.HighE.CalculateTop(6) + "px;\n";

            //Closing
            css += "}\n";

            return css;
        }
    }

    public static class CSS_StringNameE
    {
        public static string ClassName { get; private set; } = "divStringNameE";

        public static string SerializeCSS()
        {
            string css = string.Empty;

            //Opening
            css += "." + ClassName + "{\n";

            //Properties
            css += "position: absolute;\n";
            css += "top: " + (ClassicalGuitar.E.CalculateTop(6) - 9) + "px;\n";
            css += "left: " + (StyleOptions.TabInfoTextSize + (StyleOptions.TabInfoTextSize / 4)) + "px;\n";

            //Closing
            css += "}\n";

            return css;
        }
    }

    public static class CSS_StringNameD
    {
        public static string ClassName { get; private set; } = "divStringNameD";

        public static string SerializeCSS()
        {
            string css = string.Empty;

            //Opening
            css += "." + ClassName + "{\n";

            //Properties
            css += "position: absolute;\n";
            css += "top: " + (ClassicalGuitar.D.CalculateTop(6) - 9) + "px;\n";
            css += "left: " + (StyleOptions.TabInfoTextSize + (StyleOptions.TabInfoTextSize / 4)) + "px;\n";

            //Closing
            css += "}\n";

            return css;
        }
    }

    public static class CSS_StringNameA
    {
        public static string ClassName { get; private set; } = "divStringNameA";

        public static string SerializeCSS()
        {
            string css = string.Empty;

            //Opening
            css += "." + ClassName + "{\n";

            //Properties
            css += "position: absolute;\n";
            css += "top: " + (ClassicalGuitar.A.CalculateTop(6) - 9) + "px;\n";
            css += "left: " + (StyleOptions.TabInfoTextSize + (StyleOptions.TabInfoTextSize / 4)) + "px;\n";

            //Closing
            css += "}\n";

            return css;
        }
    }

    public static class CSS_StringNameG
    {
        public static string ClassName { get; private set; } = "divStringNameG";

        public static string SerializeCSS()
        {
            string css = string.Empty;

            //Opening
            css += "." + ClassName + "{\n";

            //Properties
            css += "position: absolute;\n";
            css += "top: " + (ClassicalGuitar.G.CalculateTop(6) - 9) + "px;\n";
            css += "left: " + (StyleOptions.TabInfoTextSize + (StyleOptions.TabInfoTextSize / 4)) + "px;\n";

            //Closing
            css += "}\n";

            return css;
        }
    }

    public static class CSS_StringNameB
    {
        public static string ClassName { get; private set; } = "divStringNameB";

        public static string SerializeCSS()
        {
            string css = string.Empty;

            //Opening
            css += "." + ClassName + "{\n";

            //Properties
            css += "position: absolute;\n";
            css += "top: " + (ClassicalGuitar.B.CalculateTop(6) - 9) + "px;\n";
            css += "left: " + (StyleOptions.TabInfoTextSize + (StyleOptions.TabInfoTextSize / 4)) + "px;\n";

            //Closing
            css += "}\n";

            return css;
        }
    }

    public static class CSS_StringNameHighE
    {
        public static string ClassName { get; private set; } = "divStringNameHighE";

        public static string SerializeCSS()
        {
            string css = string.Empty;

            //Opening
            css += "." + ClassName + "{\n";

            //Properties
            css += "position: absolute;\n";
            css += "top: " + (ClassicalGuitar.HighE.CalculateTop(6) - 9) + "px;\n";
            css += "left: " + (StyleOptions.TabInfoTextSize + (StyleOptions.TabInfoTextSize / 4)) + "px;\n";

            //Closing
            css += "}\n";

            return css;
        }
    }
}
