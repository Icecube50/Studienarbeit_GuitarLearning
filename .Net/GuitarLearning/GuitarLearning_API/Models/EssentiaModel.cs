using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuitarLearning_API.Models
{
    public class ChordItem
    {
        public string Chords { get; set; }
    }

    public class AudioItem
    {
        public string Audio { get; set; }
    }

    public class Temp
    {
        public float[] audioData { get; set; }
        public string chordData { get; set; }
    }
}
