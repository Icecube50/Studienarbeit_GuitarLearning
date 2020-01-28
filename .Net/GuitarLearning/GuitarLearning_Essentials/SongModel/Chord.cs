using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarLearning_Essentials.SongModel
{
    [Serializable]
    public class Chord
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public int BeatNumber { get; set; }
        public double StrokeNumber { get; set; }
        public double Duration { get; set; }

        public Chord()
        {

        }
    }
}
