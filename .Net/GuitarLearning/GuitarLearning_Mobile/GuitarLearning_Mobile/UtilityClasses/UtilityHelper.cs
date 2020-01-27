using GuitarLearning_Essentials;
using GuitarLearning_Essentials.SongModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_Mobile.UtilityClasses
{
    public class UtilityHelper
    {
        private AudioRecording AudioRecording { get; set; } = null;

        public event EventHandler JavaScript_HighlightNote;
        public event EventHandler JavaScript_HighlightChord;
        public UtilityHelper(Song song)
        {
            AudioRecording = new AudioRecording();
            AudioRecording.GetHelper().UpdateUI += ProcessResults;

            DataAnalyzer.SongEnded += OnSongEnded;

            SongHelper.InitHelper(song);
        }

        private void ProcessResults(object model, EventArgs e)
        {
            if (model is EssentiaModel)
            {
                var songObj = DataAnalyzer.CompareWithSheet(model as EssentiaModel);
                if (songObj.Type == Highlight.Chord) JavaScript_HighlightChord?.Invoke(songObj, new EventArgs());
                else if (songObj.Type == Highlight.Note) JavaScript_HighlightNote?.Invoke(songObj, new EventArgs());
            }
        }

        public void Start()
        {
            TimeHelper.SetStartTime();
            AudioRecording.StartRecording();
        }

        public void Stop()
        {
            AudioRecording.StopRecording();
        }

        private void OnSongEnded(object sender, EventArgs e)
        {
            AudioRecording.StopRecording();
        }

        public void CleanUp()
        {
            AudioRecording.CleanUp();
        }
    }
}
