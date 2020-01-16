using Android.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarLearning_Mobile.UtilityClasses
{
    public class AudioRecording
    {
        private AudioRecord audioRecorder { get; set; } = null;
        private float[] audioBuffer { get; set; } = null;
        private bool IsRecording { get; set; } = false;
        public AudioRecording()
        {
            audioBuffer = new float[100000];
            audioRecorder = new AudioRecord(
                AudioSource.Mic,
                44100,
                ChannelIn.Mono,
                Android.Media.Encoding.Pcm16bit,
                audioBuffer.Length
                );
        }

        public void StartRecording()
        {
            if (IsRecording)
                return;
            IsRecording = true;

            //TODO: Start recording and analyzing in a new thread
            var audioTask = audioRecorder.ReadAsync(audioBuffer, 0, audioBuffer.Length, 0);
            audioTask.Wait();
            var res = audioTask.Result;
            
        }

        public void StopRecording()
        {
            if (!IsRecording)
                return;
            IsRecording = false;
            audioRecorder.Stop();
        }

        public void CleanUp()
        {
            if (IsRecording)
                StopRecording();
            audioRecorder.Dispose();
        }

    }
}
