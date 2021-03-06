using System;

namespace Domains.Ordering.Events.Orders
{
    public class OrderAddedEvent : BaseEvent
    {
        public Guid ProductId { get; set; }
        public Guid ServiceMethodId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerEmail { get; set; }
        public string PreparationMethod { get; set; }
        public int? Additions { get; set; }
    }
}
