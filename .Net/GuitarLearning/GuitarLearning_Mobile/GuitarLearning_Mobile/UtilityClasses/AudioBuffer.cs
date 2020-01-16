using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarLearning_Mobile.UtilityClasses
{
    public static class AudioBuffer
    {
        private static Queue<float[]> BufferQueue { get; set; } = new Queue<float[]>();

        public static event EventHandler BufferUpdated;

        public static void Add(float[] newAudioData)
        {
            BufferQueue.Enqueue(newAudioData);
            BufferUpdated?.Invoke(null, new EventArgs());
        }

        public static float[] Get()
        {
            return BufferQueue.Dequeue();
        }

        public static void Clean()
        {
            BufferQueue.Clear();
        }
    }
}
