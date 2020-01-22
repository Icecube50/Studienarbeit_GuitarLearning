using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_TabulatorGenerator.CSS_Constants
{
    public static class CSS_Storage
    {
        public static string SerializeCss()
        {
            string css = string.Empty;

            //General information
            css += "/* Thid document was automatically generated. */\n";
            css += "/* Any changes made to the file may result in unexpected behaviour! */\n";
            css += "/* ################################################################ */\n";

            css += CSS_Header.SerializeCSS();
            css += CSS_Tabulator.SerializeCSS();

            return css;
        }
    }
}
