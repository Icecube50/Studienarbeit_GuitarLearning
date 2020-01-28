using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarLearning_Essentials
{
    public class EssentiaModel
    {
        public float[] audioData { get; set; }
        public string chordData { get; set; }
        public double time { get; set; }

        public EssentiaModel()
        {
            this.audioData = null;
            this.chordData = string.Empty;
            this.time = 0.0;
        }

        public EssentiaModel(float[] audioData, double time)
        {
            this.audioData = audioData;
            this.chordData = string.Empty;
            this.time = time;
        }

        public EssentiaModel(string chordData)
        {
            this.audioData = null;
            this.chordData = chordData;
            this.time = 0.0;
        }


    }
}
