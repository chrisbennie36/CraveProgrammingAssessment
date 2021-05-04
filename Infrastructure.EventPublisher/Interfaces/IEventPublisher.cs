namespace Infrastructure.EventPublisher.Interfaces
{
    public interface IEventPublisher<T> where T : class
    {
        void Publish(T @event);
    }
}
