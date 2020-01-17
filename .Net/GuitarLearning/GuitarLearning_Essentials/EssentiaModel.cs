using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarLearning_Essentials
{
    public class EssentiaModel
    {
        public float[] audioData { get; set; }
        public string chordData { get; set; }

        public EssentiaModel()
        {
            this.audioData = null;
            this.chordData = string.Empty;
        }

        public EssentiaModel(float[] audioData)
        {
            this.audioData = audioData;
            this.chordData = string.Empty;
        }

        public EssentiaModel(string chordData)
        {
            this.audioData = null;
            this.chordData = chordData;
        }


    }
}
