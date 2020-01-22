using GuitarLearning_TabulatorGenerator.Storage;
using GuitarLearning_TabulatorGenerator.Storage.GuitarStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_TabulatorGenerator.CSS_Constants
{
    public static class CSS_Pointer
    {
        public static string ClassName { get; private set; } = "divPointer";

        public static string Serialize()
        {
            string css = string.Empty;

            //Opening
            css += "." + ClassName + "{\n";

            //Properties
            css += "position: absolute;\n";
            css += "left: " + StyleOptions.TabInfoSize + "px;\n";
            css += "top: " + (StyleOptions.HeaderLength - 9) + "px;\n";
            css += "width: 3px;\n";
            css += "height: " + (StyleOptions.ContentLength + 10) + "px;\n";
            css += "background-color: red;\n";
            css += "z-index: 3;\n";

            //Closing
            css += "}\n";

            return css;
        }
    }
}
