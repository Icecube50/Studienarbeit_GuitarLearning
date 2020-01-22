using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_TabulatorGenerator.Storage.GuitarStrings
{
    public class String_B : IGuitarString
    {
        public override GuitarStringType StringType { get; set; }
        public override string StringName { get; set; }
        public String_B(string name)
        {
            StringName = name;
            StringType = GuitarStringType.B;
        }

        public override uint CalculateTop(uint amountOfStrings)
        {
            uint minDistance = StyleOptions.HeaderLength + 1;
            uint stringDistance = Convert.ToUInt32((StyleOptions.ContentLength / amountOfStrings) * 4);
            uint actualDistance = minDistance + stringDistance;
            return actualDistance;
        }
    }
}
