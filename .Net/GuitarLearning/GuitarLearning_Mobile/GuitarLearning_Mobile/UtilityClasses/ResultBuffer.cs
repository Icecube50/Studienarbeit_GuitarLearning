using GuitarLearning_Essentials;
using System.Collections.Generic;

namespace GuitarLearning_Mobile.UtilityClasses
{
    /// <summary>
    /// Implements a thread-safe buffer for the API data.
    /// </summary>
    public class ResultBuffer
    {
        /// <summary>
        /// Mutex, that restricts the access to the buffer-queue.
        /// </summary>
        private static readonly object _lock = new object();
        /// <summary>
        /// Instance of the queue that will be used for the buffer
        /// </summary>
        private Queue<EssentiaModel> Buffer { get; set; } = new Queue<EssentiaModel>();
        /// <summary>
        /// Add new data to the queue.
        /// </summary>
        /// <param name="model"><see cref="EssentiaModel"/> to add to the buffer.</param>
        public void Add(EssentiaModel model)
        {
            lock (_lock)
            {
                if (model == null) return;
                Buffer.Enqueue(model);
            }
        }
        /// <summary>
        /// Get the data on top of the queue.
        /// </summary>
        /// <returns><see cref="EssentiaModel"/></returns>
        public EssentiaModel Get()
        {
            lock (_lock)
            {
                return Buffer.Dequeue();
            }
        }
        /// <summary>
        /// Look at the top of the queue to determin wether an object can be dequeued.
        /// </summary>
        /// <returns><see cref="EssentiaModel"/>, <code>null</code> is returned when the buffer is emtpy.</returns>
        public EssentiaModel Peek()
        {
            lock(_lock)
            {
                return Buffer.Count != 0? Buffer.Peek() : null;
            }
        }
        /// <summary>
        /// Remove all objects inside the buffer.
        /// </summary>
        public void Clean()
        {
            lock (_lock)
            {
                Buffer.Clear();
            }
        }
    }
}
