using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_Mobile.UtilityClasses
{
    public class AudioBuffer
    {
        private readonly object _lock = new object();

        public const int BUFFER_SIZE = 1000;
        private Queue<float[]> BufferQueue { get; set; } = new Queue<float[]>();

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

        public float[] Peek()
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
}
