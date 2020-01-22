using GuitarLearning_TabulatorGenerator.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_TabulatorGenerator.CSS_Constants
{
    public static class CSS_Header
    {
        public static string ClassName { get; private set; } = "divTabulatorHeader";

        public static string SerializeCSS()
        {
            string css = string.Empty;

            //Opening
            css += "." + ClassName + "{\n";

            //Properties
            css += "position: absolute;\n";
            css += "left: 0px;\n";
            css += "top: 0px;\n";
            css += "height: " + StyleOptions.HeaderLength + "px;\n";
            css += "width: 100%;\n";

            //Closing
            css += "}\n";

            //Generate the css that is needed for the tags inside the header
            css += CSS_SongTitle.SerializeCSS();
            css += CSS_BPM.SerializeCSS();

            return css;
        }
    }

    public static class CSS_SongTitle
    {
        public static string ClassName { get; private set; } = "pSongTitle";

        public static string SerializeCSS()
        {
            string css = string.Empty;

            //Opening
            css += "." + ClassName + "{\n";

            //Properties
            css += "font-weight: bold;\n";
            css += "font-size: " + StyleOptions.TitleSize + "px;\n";
            css += "margin-left: 10px;\n";

            //Closing
            css += "}\n";

            return css;
        }
    }

    public static class CSS_BPM
    {
        public static string ClassName { get; private set; } = "pBPM";

        public static string SerializeCSS()
        {
            string css = string.Empty;

            //Opening
            css += "." + ClassName + "{\n";

            //Properties
            css += "font-weight: bold;\n";
            css += "font-size: " + StyleOptions.SubtitleSize + "px;\n";
            css += "margin-left: 10px;\n";

            //Closing
            css += "}\n";

            return css;
        }
    }
}
