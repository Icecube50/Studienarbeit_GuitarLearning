using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_TabulatorGenerator.Storage
{
    public static class StyleOptions
    {
        public static int HeaderLength { get; set; } = 100;
        public static int ContentLength { get; set; } = 120;
        public static int TitleSize => HeaderLength / 3;
        public static int SubtitleSize => HeaderLength / 5;
        public static int SizeOfQuarter { get; set; } = 20;
        public static int TabInfoSize => TabInfoTextSize * 2;
        public static int TabInfoTextSize { get; set; } = 45;
        public static string IdOfAnimatedDiv { get; set; } = "AnimatedDiv";
        public static int IntervallTime { get; set; } = 5;
        public static int ExtraSizeAfterStroke { get; set; } = 5;
    }
}
