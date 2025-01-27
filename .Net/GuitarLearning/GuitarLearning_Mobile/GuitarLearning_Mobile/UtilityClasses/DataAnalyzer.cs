﻿using GuitarLearning_Essentials;
using GuitarLearning_Mobile.ApplicationUtility;
using GuitarLearning_Mobile.DeveloperSupport;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GuitarLearning_Mobile.UtilityClasses
{
    /// <summary>
    /// Implements all methods that are used to compare the recorded data with the requirements given by the music sheet.
    /// </summary>
    public class DataAnalyzer
    {
        /// <summary>
        /// Instance of the <see cref="UtilityClasses.ResultBuffer"/> in which the audio data is stored.
        /// </summary>
        /// <value>Gets/Sets the ResultBuffer <see cref="UtilityClasses.ResultBuffer"/> field. The default value is <code>null</code>.</value>
        private ResultBuffer ResultBuffer { get; set; } = null;
        /// <summary>
        /// Store all data that was analysed so it can be logged later (hopefully faster than direct logging).
        /// </summary>
        /// <value>Gets/Sets the AnalysedData Stack field.</value>
        private Stack<SongObject> AnalysedData { get; set; } = new Stack<SongObject>();
        /// <summary>
        /// Constructor
        /// <para>Sets the <see cref="ResultBuffer"/>.</para>
        /// </summary>
        /// <param name="resultBuffer">Buffer to use.</param>
        public DataAnalyzer(ResultBuffer resultBuffer)
        {
            ResultBuffer = resultBuffer;
        }
        /// <summary>
        /// Compare the recorded data with the sheet data asynchronously.
        /// If the result of the comparison is "equal", highlight the note on the sheet.
        /// </summary>
        /// <param name="ct">Token that throws an OperationCancelledException when the token state is set to cancel. Used to stop the task.</param>
        /// <param name="webView">WebView that renders the music sheet, contians the javascript functions to highlight the notes.</param>
        public void AnalyseAsync(CancellationToken ct, WebView webView)
        {
            if (DevFlags.LoggingEnabled) Logger.AnalyzerLog("Start analysing");
            Task.Run(async () =>
            {
                try
                {
                    await Analyse(webView, ct);
                }
                catch (OperationCanceledException e)
                {
                    //Ignore
                    if (DevFlags.LoggingEnabled) Logger.AnalyzerLog("Analysing cancelled");

                    string output = "AnalysedData: [" + AnalysedData.Count + "]\n";
                    foreach (var obj in AnalysedData)
                    {
                        output += obj.Name + " " + obj.TimePosition + "\n";
                    }
                    Logger.AnalyzerLog(output);
                }
                catch (Exception e)
                {
                    Logger.Log("Analysier - Error: " + e.Message);
                    Logger.AnalyzerLog("Analysed " + AnalysedData.Count + " notes");
                }
            });
        }
        /// <summary>
        /// Used by the "AudioRecording"-page, gets the note name from the essentia model and visualises it.
        /// </summary>
        /// <param name="ct">Token to cancel the current task.</param>
        /// <param name="label">Label which will visualise the current note.</param>
        public void VisualiseApiData(CancellationToken ct, Label label)
        {
            try
            {
                Task.Run(() =>
                {
                    while (!ct.IsCancellationRequested)
                    {
                    //if (DevFlags.LoggingEnabled) Logger.AnalyzerLog("Looping");
                    if (ResultBuffer.Peek() != null)
                        {
                            ct.ThrowIfCancellationRequested();
                            var data = ResultBuffer.Get();
                            if (data != null)
                            {
                                string note = GetNoteFromData(data);

                                MainThread.BeginInvokeOnMainThread(() =>
                                {
                                    label.Text = note;
                                });
                            }
                        }
                    }
                });
            }
            catch (OperationCanceledException e)
            {
                //Ignore
            }
        }

        /// <summary>
        /// Analyse the data
        /// </summary>
        /// <param name="webView">WebView in which the sheet is rendered.</param>
        /// <param name="ct">Token which is used to cancel the task.</param>
        /// <returns></returns>
        private async Task Analyse(WebView webView, CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                //if (DevFlags.LoggingEnabled) Logger.AnalyzerLog("Looping");
                if (ResultBuffer.Peek() != null)
                {
                    if (DevFlags.LoggingEnabled) Logger.AnalyzerLog("Analysing");
                    ct.ThrowIfCancellationRequested();
                    var data = ResultBuffer.Get();
                    if (data != null)
                    {
                        var song = CompareWithSheet(data);

                        //Hack: In case something failed in CompareWithSheet skip this cycle, hopefully the next one will work
                        if (song == null) continue;
                        AnalysedData.Push(song);
                        if (song.Type == Highlight.Chord)
                        {
                            InfoContainer.HitNote();
                            if (DevFlags.LoggingEnabled) Logger.AnalyzerLog("Highlighting " + song.WebId);
                            string scriptName = $"HighlightCorrectChord('" + song.WebId + "')";

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await webView.EvaluateJavaScriptAsync(scriptName);
                            });
                        }
                        else if (song.Type == Highlight.Note)
                        {
                            InfoContainer.HitNote();
                            if (DevFlags.LoggingEnabled) Logger.AnalyzerLog("Highlighting " + song.WebId);
                            string scriptName = $"HighlightCorrectNote('" + song.WebId + "')";

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await webView.EvaluateJavaScriptAsync(scriptName);
                            });
                        }
                    }
                }
            }
            ct.ThrowIfCancellationRequested();
        }


        /// <summary>
        /// Compares the Essentia data with the musical data based on timing and value of note. Uses the <see cref="SongHelper"/>.
        /// </summary>
        /// <param name="essentiaData">Data that shall be compared with the current note.</param>
        /// <returns></returns>
        public SongObject CompareWithSheet(EssentiaModel essentiaData)
        {
            string noteToCompare = GetNoteFromData(essentiaData);
            SongObject songObject = SongHelper.GetNext();

            //SongHelper.GetNext() sometimes returns an invalid object
            //Temporary Hack (not even working...)
            if(songObject == null)
            {
                if (DevFlags.LoggingEnabled) Logger.AnalyzerLog("songObject couldn't be loaded - " + songObject.ToString());
                return null;
            }

            //if(DevFlags.LoggingEnabled) Logger.AnalyzerLog("Comparing: " + songObject.Name + " - " + noteToCompare);
            if (CompareNamesOf(songObject.Name, noteToCompare)
                && InRange(songObject.TimePosition, essentiaData.time))
            {
                if (DevFlags.LoggingEnabled) Logger.AnalyzerLog("Equal");
            }
            else 
            {
                songObject.Type = Highlight.None;
                if (DevFlags.LoggingEnabled) Logger.AnalyzerLog("Not equal");
            }

            if (SongHelper.IncreaseIndex(essentiaData.time, songObject))
            {
                //End of Song
                InfoContainer.DisplayResults();
                Logger.Log("Analyzer: END OF SONG, now crashing?");
                throw new Exception("End of Song");
            }

            return songObject;
        }
        /// <summary>
        /// Compares the timing of the musical note with the recorded note.
        /// </summary>
        /// <param name="shouldTime">Time given by the sheet, in milliseconds since the start of the song.</param>
        /// <param name="isTime">Time of recording, in milliseconds since the start of the song.</param>
        /// <returns>true when the timing is about equal, otherwise false.</returns>
        private static bool InRange(double shouldTime, double isTime)
        {
            if (DevFlags.LoggingEnabled) Logger.AnalyzerLog("Is: " + isTime + " - Should: " + shouldTime);
            bool isInRange = true;
            if (isTime < shouldTime - DevFlags.Deviation) isInRange = false;
            if (isTime > shouldTime + DevFlags.Deviation) isInRange = false;
            return isInRange;
        }
        /// <summary>
        /// Compares the name of the two notes
        /// </summary>
        /// <param name="sheetNote">Name the note has to have to be correct</param>
        /// <param name="apiNode">Name the note actually has</param>
        /// <returns>true when the names are equal</returns>
        private static bool CompareNamesOf(string sheetNote, string apiNode)
        {
            string pattern = "^" + sheetNote + "$";
            if (DevFlags.LoggingEnabled) Logger.AnalyzerLog("Comparing: " + apiNode + " with " + pattern);
            return Regex.IsMatch(apiNode, ("^" + sheetNote + "$"));
        }
        /// <summary>
        /// Splits the data string that was returned by the API and the most propable note.
        /// </summary>
        /// <param name="model">API data</param>
        /// <returns>String, name of the note that occured the most.</returns>
        private static string GetNoteFromData(EssentiaModel model)
        {
            string[] chords = model.chordData.Split(';');
            var occurence = GetOccurence(chords);
            return GetChordFromOccurence(occurence);
        }
        /// <summary>
        /// Create a dictionary with one entry for each occuring Note. Increase the value on each reoccurence of a note.
        /// </summary>
        /// <param name="chords">array of all the chords inside the chord-string.</param>
        /// <returns>Dictionary[ChordName, NumberOfOccurence]</returns>
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
        /// <summary>
        /// Find the name of the note that occured the most.
        /// </summary>
        /// <param name="occurence">The dictionary that shall be analysed.</param>
        /// <returns>String, name of the note that occured the most.</returns>
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
