﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_TabulatorGenerator.Storage.GuitarStrings
{
    public class String_D : IGuitarString
    {
        public override GuitarStringType StringType { get; set; }
        public override string StringName { get; set; }
        public String_D(string name)
        {
            StringName = name;
            StringType = GuitarStringType.D;
        }

        public override uint CalculateTop(uint amountOfStrings)
        {
            uint minDistance = StyleOptions.HeaderLength + 1;
            uint stringDistance = Convert.ToUInt32((StyleOptions.ContentLength / amountOfStrings) * 2);
            uint actualDistance = minDistance + stringDistance;
            return actualDistance;
        }
    }
}
