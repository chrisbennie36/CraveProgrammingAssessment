using MediatR;
using System;

namespace Domains.Ordering.Events
{
    public class BaseEvent : INotification
    {
        public Guid Id { get; set; }
    }
}
