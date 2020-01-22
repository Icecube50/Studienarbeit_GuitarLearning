using GuitarLearning_TabulatorGenerator.CSS_Constants;
using GuitarLearning_TabulatorGenerator.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_TabulatorGenerator.HTML_Serializing
{
    public interface I_HTML_Object
    {
        string Serialize();
    }

    public class HTML_Document : I_HTML_Object
    {
        private string Title { get; set; }
        private string Style { get; set; }
        private List<I_HTML_Object> HtmlContent { get; set; }
        public HTML_Document(string title, string stylesheet)
        {
            Title = title;
            Style = stylesheet;
            HtmlContent = new List<I_HTML_Object>();
        }

        public void AddContent(I_HTML_Object obj)
        {
            HtmlContent.Add(obj);
        }

        public string Serialize()
        {
            string html = string.Empty;

            //General Info
            html += "<!-- Thid document was automatically generated. -->\n";
            html += "<!-- Any changes made to the file may result in unexpected behaviour! -->\n";
            html += "<!-- ################################################################ -->\n";

            //Header
            html += "<!DOCTYPE html>\n";
            html += "<html lang=\"de\">\n";
            html += "<head>\n";
            html += "<meta charset=\"utf-8\">\n";
            html += "<style>\n";
            html += CSS_Storage.SerializeCss();
            html += "</style>\n";
            if (Title != string.Empty) html += "<title>" + Title + "</title>\n";
            html += "</head>\n";

            //Body
            html += "<body>\n";
            html += SerializeContent();
            //Content

            //Script
            html += "<script type=\"text/javascript\">\n";
            html += GetSkript();
            html += "</script>\n";

            html += "</body>\n";

            //EOF
            html += "</html>\n";

            return html;
        }

        private string SerializeContent()
        {
            string html = string.Empty;
            foreach(I_HTML_Object htmlObj in HtmlContent)
            {
                html += htmlObj.Serialize();
            }
            return html;
        }

        private string GetSkript()
        {
            string js = string.Empty;

            js += "function Animate() {\n";
            js += "var tab = document.getElementById(\"" + StyleOptions.IdOfAnimatedDiv + "\");\n";
            js += "posX = 0;\n";
            js += "var id = setInterval(frame, " + StyleOptions.IntervallTime + ");\n";
            js += "\n";
            js += "function frame() {\n";
            js += "if(posX == -2000) {\n"; //TODO: Get the value by comparing screen-size and song-size
            js += "clearInterval(id);\n";
            js += "}\n";
            js += "else {\n";
            js += "posX--;\n";
            js += "tab.style.left = posX + \"px\";\n";
            js += "}\n";
            js += "}\n";
            js += "}\n";

            return js;
        }
    }
}
