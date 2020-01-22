using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_TabulatorGenerator.HTML_Serializing
{
    public class HTML_RythmDiv : I_HTML_Object
    {
        private string HtmlClass { get; set; }
        private string ID { get; set; }
        private string Text { get; set; }
        public HTML_RythmDiv(string htmlClass, string id, string text)
        {
            HtmlClass = htmlClass;
            ID = id;
            Text = text;
        }

        public string Serialize()
        {
            string html = string.Empty;

            //Opening Tag
            html += "<div";
            if (HtmlClass != string.Empty) html += " class=\"" + HtmlClass + "\"";
            if (ID != string.Empty) html += " id=\"" + ID + "\"";
            html += ">";

            //Content
            html += Text;

            //Closing Tag
            html += "</div>\n";
            return html;
        }
    }
}
