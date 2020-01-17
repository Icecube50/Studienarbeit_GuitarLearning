using Android.Media;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_Mobile.UtilityClasses
{
    public class AudioRecording
    {
        public event EventHandler StartProcessing;
        public event EventHandler StopProcessing;

        private AudioRecord audioRecorder { get; set; } = null;
        private float[] audioBuffer { get; set; } = null;
        private bool IsRecording { get; set; } = false;

        private AudioBuffer _audioBuffer { get; set; } = null;
        private API_Helper _Helper { get; set; } = null;

        public AudioRecording()
        {
            audioBuffer = new float[AudioBuffer.BUFFER_SIZE];
            audioRecorder = new AudioRecord(
                AudioSource.Mic,
                44100,
                ChannelIn.Mono,
                Android.Media.Encoding.PcmFloat,
                audioBuffer.Length
                );

            _audioBuffer = new AudioBuffer(this);
            _Helper = new API_Helper(_audioBuffer);

            StartProcessing += async (s, e) =>
            {
                await ReadDataToBuffer();
            };
        }

        public void StartRecording()
        {
            if (IsRecording)
                return;
            IsRecording = true;
            StartProcessing?.Invoke(this, new EventArgs());
            
        }

        public void StopRecording()
        {
            if (!IsRecording)
                return;
            IsRecording = false;
            StopProcessing?.Invoke(this, new EventArgs());
        }

        public void CleanUp()
        {
            if (IsRecording)
                StopRecording();
            audioRecorder.Dispose();
        }

        private async Task ReadDataToBuffer()
        {
            audioRecorder.StartRecording();
            while (IsRecording)
            {
                await audioRecorder.ReadAsync(audioBuffer, 0, audioBuffer.Length, 0);
                _audioBuffer.Add(audioBuffer);
                audioBuffer = new float[audioBuffer.Length];
            }
            audioRecorder?.Stop();
        }

    }
}
