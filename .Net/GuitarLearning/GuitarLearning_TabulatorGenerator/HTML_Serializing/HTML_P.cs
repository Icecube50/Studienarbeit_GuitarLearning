using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_TabulatorGenerator.HTML_Serializing
{
    public class HTML_P : I_HTML_Object
    {
        private string Text { get; set; }
        private string HtmlClass { get; set; }
        private string ID { get; set; }

        public HTML_P(string text, string htmlClass, string id)
        {
            Text = text;
            HtmlClass = htmlClass;
            ID = id;
        }

        public string Serialize()
        {
            string html = string.Empty;

            //Opening Tag
            html += "<p";
            if (HtmlClass != string.Empty) html += " class=\"" + HtmlClass + "\"";
            if (ID != string.Empty) html += " id=\"" + ID + "\"";
            html += ">\n";

            //Content
            html += Text + "\n";

            //Closing Tag
            html += "</p>\n";

            return html;
        }
    }
}
