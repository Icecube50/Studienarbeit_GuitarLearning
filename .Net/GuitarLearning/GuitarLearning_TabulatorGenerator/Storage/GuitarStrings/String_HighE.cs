using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_TabulatorGenerator.Storage.GuitarStrings
{
    public class String_HighE : IGuitarString
    {
        public override GuitarStringType StringType { get; set; }
        public override string StringName { get; set; }
        public String_HighE(string name)
        {
            StringName = name;
            StringType = GuitarStringType.e;
        }

        public override int CalculateTop(int amountOfStrings)
        {
            int minDistance = 1;
            int stringDistance = Convert.ToInt32((StyleOptions.ContentLength / amountOfStrings) * 5);
            int actualDistance = minDistance + stringDistance;
            return actualDistance;
        }
    }
}
