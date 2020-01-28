using GuitarLearning_Essentials;
using GuitarLearning_Essentials.SongModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GuitarLearning_Mobile.UtilityClasses
{
    public class DataAnalyzer
    {
        public const double DEVIATION = 300;
        private ResultBuffer ResultBuffer { get; set; } = null;
        public DataAnalyzer(ResultBuffer resultBuffer)
        {
            ResultBuffer = resultBuffer;
        }

        public void AnalyseAsync(CancellationToken ct, WebView webView)
        {
            Task.Run(async () =>
            {
                try
                {
                    while (!ct.IsCancellationRequested)
                    {
                        if (ResultBuffer.Peek() != null)
                        {
                            ct.ThrowIfCancellationRequested();
                            var data = ResultBuffer.Get();
                            var song = CompareWithSheet(data);

                            if (song.Type == Highlight.Chord) 
                            {
                                string scriptName = $"HighlightCorrectChord('" + song.WebId + "')";
                                await webView.EvaluateJavaScriptAsync(scriptName);
                            }
                            else if (song.Type == Highlight.Note)
                            {
                                string scriptName = $"HighlightCorrectNote('" + song.WebId + "')";
                                await webView.EvaluateJavaScriptAsync(scriptName);
                            }
                        }
                    }
                    ct.ThrowIfCancellationRequested();
                }
                catch (OperationCanceledException e)
                {
                    //Ignore
                }
                catch (Exception e)
                {
                    Logger.Log("Analysier - Error: " + e.Message);
                }
            });
        }



        public SongObject CompareWithSheet(EssentiaModel essentiaData)
        {
            string noteToCompare = GetNoteFromData(essentiaData);
            SongObject songObject = SongHelper.GetNext();

            Logger.Log("Comparing: " + songObject.Name + " - " + noteToCompare);
            if (songObject.Name.Contains(noteToCompare)
                && InRange(songObject.TimePosition, essentiaData.time)) { }
            else { songObject.Type = Highlight.None; }

            if (SongHelper.IncreaseIndex(essentiaData.time, songObject))
            {
                //End of Song
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
