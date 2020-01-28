using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_Mobile.UtilityClasses
{
    public class AudioBuffer
    {
        private readonly object _lock = new object();

        public const int BUFFER_SIZE = 10000;
        private Queue<AudioData> BufferQueue { get; set; } = new Queue<AudioData>();

        public void Add(AudioData newAudioData)
        {
            lock (_lock)
            {
                BufferQueue.Enqueue(newAudioData);
            }
        }

        public AudioData Get()
        {
            lock (_lock)
            {
                return BufferQueue.Dequeue();
            }
        }

        public AudioData Peek()
        {
            lock (_lock)
            {
                return BufferQueue.Count != 0? BufferQueue.Peek() : null;
            }
        }

        public int Count()
        {
            return BufferQueue.Count;
        }

        public void Clean()
        {
            lock (_lock)
            {
                BufferQueue.Clear();
            }
        }
    }

    public class AudioData
    {
        public float[] Data { get; set; }
        public double Time { get; set; }
        public AudioData(float[] data, double time)
        {
            Data = data;
            Time = time;
        }
    }
}
