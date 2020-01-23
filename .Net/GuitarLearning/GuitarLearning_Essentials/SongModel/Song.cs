using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarLearning_Essentials.SongModel
{
    [Serializable]
    public class Song
    {
        public string SongTitle { get; set; }
        public int BPM { get; set; }
        public double SongDuration { get; set; }
        public List<Chord> NotationChords { get; set; }
        public List<Note> Notation { get; set; }
    }
}
