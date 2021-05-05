using MediatR;
using System;

namespace Domains.Orders.Events.ServiceMethods
{
    public class ServiceMethodAddedEvent : INotification
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
