using MediatR;
using System;

namespace Domains.Orders.Events
{
    public class OrderAddedEvent : INotification
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid ServiceMethodId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerEmail { get; set; }
        public string AdditionalInstructions { get; set; }
    }
}
