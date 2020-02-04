using GuitarLearning_Essentials;
using GuitarLearning_Mobile.UtilityClasses;
using Plugin.Permissions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuitarLearning_Mobile.Pages
{
    /// <summary>
    /// CURRENTLY NOT WORKING DUE TO CHANGES TO <see cref="UtilityClasses"/>.
    /// DO NOT USE!
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pgAudioRecording : ContentPage
    {
        AudioBuffer audioBuffer;
        ResultBuffer resultBuffer;
        AudioRecording audioRecording;
        API_Helper apiHelper;
        DataAnalyzer dataAnalyzer;
        CancellationTokenSource cts;

        bool recordingState = false;

        private event EventHandler RecordingStateChanged;
        public pgAudioRecording()
        {
            InitializeComponent();

            audioBuffer = new AudioBuffer();
            resultBuffer = new ResultBuffer();

            audioRecording = new AudioRecording(audioBuffer);
            apiHelper = new API_Helper(audioBuffer, resultBuffer);
            dataAnalyzer = new DataAnalyzer(resultBuffer);

            cts = new CancellationTokenSource();

            RecordingStateChanged += OnRecordingStateChanged;
        }

        private void OnRecordingStateChanged(object sender, EventArgs e)
        {
            if (recordingState)
            {
                StartProcessing();
                btnRecording.Text = "Stop";
                lbRecordingState.Text = "Recording = ON";
            }
            else
            {
                StopProcessing();
                lbCurrentChord.Text = "No Chord Detected";
                btnRecording.Text = "Start";
                lbRecordingState.Text = "Recording = OFF";
                cts.Dispose();
                cts = new CancellationTokenSource();
            }
        }

        private void StartProcessing()
        {
            TimeHelper.SetStartTime();

            audioRecording.StartRecording();
            apiHelper.StartAPI();
            dataAnalyzer.VisualiseApiData(cts.Token, lbCurrentChord);
        }

        private void StopProcessing()
        {
            audioRecording.StopRecording();
            apiHelper.StopAPI();

            cts.Cancel();

            resultBuffer.Clean();
            audioBuffer.Clean();
        }

        private void OnLeave(object sender, EventArgs e)
        {
            StopProcessing();
        }

        private void Start_Clicked(object sender, EventArgs e)
        {
            recordingState = !recordingState;
            RecordingStateChanged?.Invoke(null, new EventArgs());
        }
    }
}