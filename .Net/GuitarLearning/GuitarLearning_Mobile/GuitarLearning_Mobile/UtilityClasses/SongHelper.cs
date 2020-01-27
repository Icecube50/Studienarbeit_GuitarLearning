using GuitarLearning_Essentials.SongModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarLearning_Mobile.UtilityClasses
{
    public static class SongHelper
    {
        private static Song Song { get; set; }
        private static int SongIndex { get; set; }
        private static double BPS { get; set; }
        public static void InitHelper(Song song)
        {
            Song = song;
            SongIndex = 0;
            BPS = song.BPM != 0 ? ((60 / song.BPM) * 1000) : 0;

            if (SongIndex >= Song.Notation.Count) throw new Exception("Invalid Index");
        }

        public static bool IncreaseIndex(double elapsed, SongObject obj)
        {
            if ((obj.TimePosition + obj.Duration) <= elapsed) { SongIndex++; }
            if (SongIndex < Song.Notation.Count) return true;
            else return false;
        }

        public static SongObject GetNext()
        {
            var obj = Song.Notation[SongIndex];
            if (obj.Note != null) return GetNext_Note(obj.Note);
            else return GetNext_Chord(obj.Chord);
        }

        private static SongObject GetNext_Note(Note note)
        {
            var songObj = new SongObject();
            songObj.Name = note.Name;
            songObj.Type = Highlight.Note;
            songObj.WebId = note.ID;
            songObj.TimePosition = CalculatePosition(note.StrokeNumber, note.BeatNumber);
            songObj.Duration = BPS * note.Duration;
            return songObj;
        }

        private static SongObject GetNext_Chord(Chord chord)
        {
            var songObj = new SongObject();
            songObj.Name = chord.Name;
            songObj.Type = Highlight.Chord;
            songObj.WebId = chord.ID;
            songObj.TimePosition = CalculatePosition(chord.StrokeNumber, chord.BeatNumber);
            songObj.Duration = BPS * chord.Duration;
            return songObj;
        }

        private static int CalculatePosition(double StrokeNumber, double BeatNumber)
        {
            double timePerBeat = 4 * BPS;
            double positionTime = timePerBeat * BeatNumber;
            positionTime += (BPS * StrokeNumber);
            positionTime = Math.Round(positionTime);
            return Convert.ToInt32(positionTime);
        }
    }

    public class SongObject
    {
        public int TimePosition { get; set; }
        public string Name { get; set; }
        public Highlight Type { get; set; }
        public string WebId { get; set; }
        public double Duration { get; set; }
    }
}
