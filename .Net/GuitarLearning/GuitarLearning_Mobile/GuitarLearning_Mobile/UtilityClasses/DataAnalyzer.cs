using GuitarLearning_Essentials;
using GuitarLearning_Essentials.SongModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarLearning_Mobile.UtilityClasses
{
    public static class DataAnalyzer
    {
        public const double DEVIATION = 300;
        public static SongObject CompareWithSheet(EssentiaModel essentiaData)
        {
            string noteToCompare = GetNoteFromData(essentiaData);
            SongObject songObject = SongHelper.GetNext();
            double elapsed = TimeHelper.GetElapsedTime();

            if (songObject.Name.Contains(noteToCompare)
                && InRange(songObject.TimePosition, elapsed)) { }
            else { songObject.Type = Highlight.None; }

            if (SongHelper.IncreaseIndex(elapsed, songObject))
            {
                //Invoke Stop Event
            }

            return songObject;
        }

        private static bool InRange(double shouldTime, double isTime)
        {
            bool isInRange = true;
            if (isTime < shouldTime - DEVIATION) isInRange = false;
            if (isTime > shouldTime + DEVIATION) isInRange = false;
            return isInRange;
        }

        private static string GetNoteFromData(EssentiaModel model)
        {
            string[] chords = model.chordData.Split(';');
            var occurence = GetOccurence(chords);
            return GetChordFromOccurence(occurence);
        }

        private static Dictionary<string, int> GetOccurence(string[] chords)
        {
            Dictionary<string, int> ChordOccurence = new Dictionary<string, int>();
            foreach (string chord in chords)
            {
                string key = (chord.Split(','))[0];
                if (!ChordOccurence.ContainsKey(key))
                {
                    ChordOccurence.Add(key, 1);
                }
                else
                {
                    ChordOccurence[key]++;
                }
            }
            return ChordOccurence;
        }

        private static string GetChordFromOccurence(Dictionary<string, int> occurence)
        {
            KeyValuePair<string, int> max;
            bool first = true;
            foreach (var kvp in occurence)
            {
                if (first)
                {
                    max = kvp;
                    first = !first;
                    continue;
                }

                if (kvp.Value > max.Value)
                    max = kvp;
            }
            return max.Key;
        }
    }
}
