using Android.Media;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GuitarLearning_Mobile.UtilityClasses
{
    public class AudioRecording
    {
        private AudioRecord AudioRecorder { get; set; } = null;
        private float[] InputBuffer { get; set; } = null;
        private AudioBuffer AudioBuffer { get; set; } = null;
        CancellationTokenSource cts;
        public AudioRecording(AudioBuffer audioBuffer)
        {
            InputBuffer = new float[AudioBuffer.BUFFER_SIZE];
            AudioRecorder = new AudioRecord(
                AudioSource.Mic,
                44100,
                ChannelIn.Mono,
                Android.Media.Encoding.PcmFloat,
                InputBuffer.Length
                );

            AudioBuffer = audioBuffer;
            cts = new CancellationTokenSource();
        }

        public void StartRecording()
        {
            Task.Run(async () =>
            {
                try
                {
                    await ReadDataToBuffer(cts.Token);
                }
                catch (OperationCanceledException e)
                {
                    //Ignore, just return
                }
                catch (Exception e)
                {
                    Logger.Log("Recorder - Error: " + e.Message);
                }
            });
        }

        public void StopRecording()
        {
            if (!cts.IsCancellationRequested)
            {
                cts.Cancel();
            }
        }

        public void CleanUp()
        {
            SafeGetRecorder()?.Release();
            AudioBuffer?.Clean();
        }

        private object _lock = new object();
        private AudioRecord SafeGetRecorder()
        {
            lock(_lock)
            {
                return this.AudioRecorder;
            }
        }

        private async Task ReadDataToBuffer(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            SafeGetRecorder().StartRecording();
            while (!ct.IsCancellationRequested)
            {
                await SafeGetRecorder().ReadAsync(InputBuffer, 0, InputBuffer.Length, 0);
                AudioBuffer?.Add(new AudioData(InputBuffer, TimeHelper.GetElapsedTime()));
                InputBuffer = new float[InputBuffer.Length];
            }
            SafeGetRecorder()?.Stop();
            ct.ThrowIfCancellationRequested();
        }

    }
}
