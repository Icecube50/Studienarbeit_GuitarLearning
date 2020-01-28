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
        public List<INote> Notation { get; set; } = new List<INote>();

        public Song()
        {

        }
    }

    [Serializable]
    public class INote
    {
        public Note Note { get; set; } = new Note();
        public Chord Chord { get; set; } = new Chord();
        public INote(Note note)
        {
            Note = note;
            Chord = null;
        }
        public INote(Chord chord)
        {
            Chord = chord;
            Note = null;
        }
        public INote()
        {

        }
    }
}
