using Infrastructure.EventPublisher.Interfaces;
using MediatR;
using System;

namespace Infrastructure.EventPublisher
{
    public class EventPublisher : IEventPublisher<INotification>
    {
        private readonly IQueue<INotification> _notificationQueue;

        public EventPublisher(IQueue<INotification> queue)
        {
            _notificationQueue = queue ?? throw new ArgumentNullException(nameof(queue));
        }

        public void Publish(INotification @event)
        {
            _notificationQueue.Enqueue(@event);
        }
    }
}
