namespace Infrastructure.EventPublisher.Interfaces
{
    public interface IQueue<T>
    {
        void Enqueue(T item);
        T Dequeue();
    }
}
