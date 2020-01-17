using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarLearning_Mobile.UtilityClasses
{
    public class AudioBuffer
    {
        public event EventHandler StartProcessing;
        public event EventHandler StopProcessing;

        private readonly object _lock = new object();

        public const int BUFFER_SIZE = 1000;
        private Queue<float[]> BufferQueue { get; set; } = new Queue<float[]>();
        public AudioBuffer(AudioRecording recorder)
        {
            recorder.StartProcessing += Recorder_StartProcessing;
            recorder.StopProcessing += Recorder_StopProcessing;
        }

        private void Recorder_StopProcessing(object sender, EventArgs e)
        {
            StopProcessing?.Invoke(this, new EventArgs());
            Clean();
        }

        private void Recorder_StartProcessing(object sender, EventArgs e)
        {
            StartProcessing?.Invoke(this, new EventArgs());
        }

        public void Add(float[] newAudioData)
        {
            lock (_lock)
            {
                BufferQueue.Enqueue(newAudioData);
            }
        }

        public float[] Get()
        {
            lock (_lock)
            {
                return BufferQueue.Dequeue();
            }
        }

        public int Count()
        {
            lock (_lock)
            {
                return BufferQueue.Count;
            }
        }

        public void Clean()
        {
            lock (_lock)
            {
                BufferQueue.Clear();
            }
        }
    }
}
