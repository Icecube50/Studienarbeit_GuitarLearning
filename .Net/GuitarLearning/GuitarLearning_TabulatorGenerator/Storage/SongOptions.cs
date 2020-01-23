﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_TabulatorGenerator.Storage
{
    public static class SongOptions
    {
        public static string SongTitle { get; set; } = "Unkown";
        public static string BPM { get; private set; } = "-1 BPM";
        public static int BpmValue { get; private set; } = 0;

        public static void SetBPM(int bpm)
        {
            if (bpm > 200) bpm = 200;
            BPM = Convert.ToString(bpm) + " BPM";
            BpmValue = bpm;
        }
    }
}
