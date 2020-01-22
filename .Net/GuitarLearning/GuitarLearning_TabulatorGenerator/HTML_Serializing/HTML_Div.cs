using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_TabulatorGenerator.HTML_Serializing
{
    public class HTML_Div : I_HTML_Object
    {
        private string HtmlClass { get; set; }
        private string ID { get; set; }
        private List<I_HTML_Object> Content { get; set; }
        public HTML_Div(string htmlClass, string id)
        {
            HtmlClass = htmlClass;
            ID = id;
            Content = new List<I_HTML_Object>();
        }

        public void AddContent(I_HTML_Object obj)
        {
            Content.Add(obj);
        }

        public string Serialize()
        {
            string html = string.Empty;

            //Opening Tag
            html += "<div";
            if (HtmlClass != string.Empty) html += " class=\"" + HtmlClass + "\"";
            if (ID != string.Empty) html += " id=\"" + ID + "\"";
            html += ">\n";

            //Content
            html += SerializeContent();

            //Closing Tag
            html += "</div>\n";
            return html;
        }

        private string SerializeContent()
        {
            string html = string.Empty;
            foreach(I_HTML_Object htmlObj in Content)
            {
                html += htmlObj.Serialize();
            }
            return html;
        }
    }
}
