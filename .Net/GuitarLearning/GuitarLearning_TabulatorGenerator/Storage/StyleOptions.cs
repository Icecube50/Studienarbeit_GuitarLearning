using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_TabulatorGenerator.Storage
{
    public static class StyleOptions
    {
        public static uint HeaderLength { get; set; } = 100;
        public static uint ContentLength { get; set; } = 120;
        public static uint TitleSize => HeaderLength / 3;
        public static uint SubtitleSize => HeaderLength / 5;
        public static uint SizeOfQuarter { get; set; } = 20;
    }
}
