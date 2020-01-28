using GuitarLearning_Essentials;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarLearning_Mobile.UtilityClasses
{
    public class ResultBuffer
    {
        private object _lock = new object();
        private Queue<EssentiaModel> Buffer { get; set; } = new Queue<EssentiaModel>();
        public void Add(EssentiaModel model)
        {
            lock (_lock)
            {
                if (model == null) return;
                Buffer.Enqueue(model);
            }
        }

        public EssentiaModel Get()
        {
            lock (_lock)
            {
                return Buffer.Dequeue();
            }
        }

        public EssentiaModel Peek()
        {
            lock(_lock)
            {
                return Buffer.Count != 0? Buffer.Peek() : null;
            }
        }

        public void Clean()
        {
            lock (_lock)
            {
                Buffer.Clear();
            }
        }
    }
}
