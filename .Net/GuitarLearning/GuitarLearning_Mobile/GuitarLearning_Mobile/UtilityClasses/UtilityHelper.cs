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
    public class UtilityHelper
    {
        private AudioRecording AudioRecording { get; set; } = null;
        private AudioBuffer AudioBuffer { get; set; } = null;
        private API_Helper API_Helper { get; set; } = null;
        private ResultBuffer ResultBuffer { get; set; } = null;
        private DataAnalyzer DataAnalyzer { get; set; } = null;
        private CancellationTokenSource cts;

        public UtilityHelper(Song song)
        {
            //Init Buffer
            AudioBuffer = new AudioBuffer();
            ResultBuffer = new ResultBuffer();

            //Init Classes
            AudioRecording = new AudioRecording(AudioBuffer);
            API_Helper = new API_Helper(AudioBuffer, ResultBuffer);
            DataAnalyzer = new DataAnalyzer(ResultBuffer);

            SongHelper.InitHelper(song);

            cts = new CancellationTokenSource();
        }

        public void Start(WebView webView)
        {
            TimeHelper.SetStartTime();

            AudioRecording.StartRecording();
            API_Helper.StartAPI();
            DataAnalyzer.AnalyseAsync(cts.Token, webView);
        }

        public void Stop()
        {
            AudioRecording.StopRecording();
            API_Helper.StopAPI();
            cts.Cancel();

            EmptyBuffer();
        }

        private void EmptyBuffer()
        {
            AudioBuffer.Clean();
            ResultBuffer.Clean();
        }

        public void CleanUp()
        {
            AudioRecording.CleanUp();
        }
    }
}
