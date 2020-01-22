using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_TabulatorGenerator.HTML_Serializing
{
    public class HTML_NoteDiv : I_HTML_Object
    {
        private string HtmlClass { get; set; }
        private string ID { get; set; }
        private string NoteValue { get; set; }
        public HTML_NoteDiv(string htmlClass, string id, string noteValue)
        {
            HtmlClass = htmlClass;
            ID = id;
            NoteValue = noteValue;
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
            html += NoteValue;

            //Closing Tag
            html += "</div>\n";
            return html;
        }
    }
}
