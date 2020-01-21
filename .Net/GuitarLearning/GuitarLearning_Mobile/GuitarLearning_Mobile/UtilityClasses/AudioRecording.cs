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

        private AudioRecord AudioRecorder { get; set; } = null;
        private float[] InputBuffer { get; set; } = null;
        private bool IsRecording { get; set; } = false;

        private AudioBuffer AudioBuffer { get; set; } = null;
        private API_Helper Helper { get; set; } = null;

        public AudioRecording()
        {
            InputBuffer = new float[AudioBuffer.BUFFER_SIZE];
            AudioRecorder = new AudioRecord(
                AudioSource.Mic,
                44100,
                ChannelIn.Mono,
                Android.Media.Encoding.PcmFloat,
                InputBuffer.Length
                );

            AudioBuffer = new AudioBuffer();
            Helper = new API_Helper(AudioBuffer);

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
            Helper.StartAPI();
        }

        public void StopRecording()
        {
            if (!IsRecording)
                return;
            IsRecording = false;
            Helper.StopAPI();
        }

        public void CleanUp()
        {
            if (IsRecording)
                StopRecording();
            AudioRecorder.Dispose();
            AudioBuffer.Clean();
            Helper.StopAPI();
        }

        public API_Helper GetHelper()
        {
            return Helper;
        }

        private async Task ReadDataToBuffer()
        {
            AudioRecorder.StartRecording();
            while (IsRecording)
            {
                await AudioRecorder.ReadAsync(InputBuffer, 0, InputBuffer.Length, 0);
                AudioBuffer.Add(InputBuffer);
                InputBuffer = new float[InputBuffer.Length];
            }
            AudioRecorder?.Stop();
        }

    }
}
