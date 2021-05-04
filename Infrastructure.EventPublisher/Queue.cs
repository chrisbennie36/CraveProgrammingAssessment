using Infrastructure.EventPublisher.Interfaces;
using System.Collections.Concurrent;

namespace Infrastructure.EventPublisher
{
    public class Queue<T> : IQueue<T> where T : class
    {
        private readonly ConcurrentQueue<T> queue;

        public Queue()
        {
            queue = new ConcurrentQueue<T>();
        }

        public void Enqueue(T item)
        {
            this.queue.Enqueue(item);
        }

        public T Dequeue()
        {
            T result = null;
            queue.TryDequeue(out result);
            return result;
        }
    }
}
