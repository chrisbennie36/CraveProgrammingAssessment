using Newtonsoft.Json.Linq;
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
        public JObject AdditionalModifiers { get; set; }
    }
}
