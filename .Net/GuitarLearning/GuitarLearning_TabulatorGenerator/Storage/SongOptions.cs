using GuitarLearning_Essentials.SongModel;
using GuitarLearning_TabulatorGenerator.MusicalNotes;
using GuitarLearning_TabulatorGenerator.Storage.GuitarStrings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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

        private static Song GetSongModel()
        {
            var song = new Song();
            song.SongTitle = SongTitle;
            song.BPM = BpmValue;
            song.SongDuration = MusicalStorage.SongDurationInMS();
            song.Notation = new List<Note>();
            song.NotationChords = new List<Chord>();

            int beatCounter = -1; //2 strokes in the beginning, but we want to start with value 1
            double strokeCounter = 1;
            foreach (var musicalNote in MusicalStorage.Melodie)
            {
                if (musicalNote is MusicalNote_Stroke) { beatCounter++; if(strokeCounter >= 4) strokeCounter -= 4; }
                else if (musicalNote is MusicalNote_Chord)
                {
                    var chord = musicalNote as MusicalNote_Chord;
                    var notationChord = new Chord();

                    notationChord.ID = chord.NoteID;
                    notationChord.Duration = chord.GetMusicalDuration();
                    notationChord.BeatNumber = beatCounter;
                    notationChord.StrokeNumber = strokeCounter;
                    notationChord.Name = chord.ChordName;
                    song.NotationChords.Add(notationChord);

                    strokeCounter += notationChord.Duration;
                }
                else
                {
                    var notationNote = new Note();
                    notationNote.ID = musicalNote.NoteID;
                    notationNote.Name = MusicalCalculator.GetNameFromPosition(ClassicalGuitar.ObjToEnum(musicalNote.StringOfNote), musicalNote.NoteValue);
                    notationNote.Duration = musicalNote.GetMusicalDuration();
                    notationNote.BeatNumber = beatCounter;
                    notationNote.StrokeNumber = strokeCounter;
                    song.Notation.Add(notationNote);

                    strokeCounter += notationNote.Duration;    
                }
            }
            return song;
        }

        public static void SerializeJson(string file)
        {
            var song = GetSongModel();
            //string json = JsonConvert.SerializeObject(song);
            //File.WriteAllText(file, json);

            using (var writer = new StreamWriter(file, false))
            {
                var serializer = new XmlSerializer(song.GetType());
                serializer.Serialize(writer, song);
            }
        }
    }
}
