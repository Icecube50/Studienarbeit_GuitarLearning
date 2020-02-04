using System.Collections.Generic;

namespace GuitarLearning_Mobile.UtilityClasses
{
    /// <summary>
    /// Implements a thread-safe buffer for the raw audio data.
    /// </summary>
    public class AudioBuffer
    {
        /// <summary>
        /// Mutext, that restricts the access to the buffer-queue.
        /// </summary>
        private static readonly object _lock = new object();
        /// <summary>
        /// Constant, limits the amount of data that can be recorded and processed at once.
        /// </summary>
        public static int BUFFER_SIZE = 5000;
        /// <summary>
        /// Instance of the queue that will be used as buffer.
        /// </summary>
        /// <value>Gets/Sets the BufferQueue field.</value>
        private Queue<AudioData> BufferQueue { get; set; } = new Queue<AudioData>();

        /// <summary>
        /// Adds new data to the queue.
        /// </summary>
        /// <param name="newAudioData"><see cref="AudioData"/> to add to the queue.</param>
        public void Add(AudioData newAudioData)
        {
            lock (_lock)
            {
                BufferQueue.Enqueue(newAudioData);
            }
        }
        /// <summary>
        /// Get the the data on top of the queue.
        /// </summary>
        /// <returns><see cref="AudioData"/></returns>
        public AudioData Get()
        {
            lock (_lock)
            {
                return BufferQueue.Dequeue();
            }
        }
        /// <summary>
        /// Look at the top of the queue to determin whether an object can be dequed.
        /// </summary>
        /// <returns><see cref="AudioData"/>, null is returned when the buffer is empty.</returns>
        public AudioData Peek()
        {
            lock (_lock)
            {
                return BufferQueue.Count != 0? BufferQueue.Peek() : null;
            }
        }

        /// <summary>
        /// Get the amount of objects inside the buffer.
        /// </summary>
        /// <returns>int</returns>
        public int Count()
        {
            return BufferQueue.Count;
        }

        /// <summary>
        /// Delete all objects inside the buffer.
        /// </summary>
        public void Clean()
        {
            lock (_lock)
            {
                BufferQueue.Clear();
            }
        }
    }

    /// <summary>
    /// Class that stores all important data, which is needed to analyse the recorded audio frame.
    /// </summary>
    public class AudioData
    {
        /// <summary>
        /// The Data property stores a float[] that contains the raw audio data.
        /// </summary>
        /// <value>Gets/Sets the value of the Data property float[] field.</value>
        public float[] Data { get; set; }
        /// <summary>
        /// Point in time on which the audio data was recorded.
        /// </summary>
        /// <value>Gets/Sets the Time property double field. Time is stored as milliseconds in double.</value>
        public double Time { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">Raw audio data.</param>
        /// <param name="time">Time of recording since start-time.</param>
        public AudioData(float[] data, double time)
        {
            Data = data;
            Time = time;
        }
    }
}
