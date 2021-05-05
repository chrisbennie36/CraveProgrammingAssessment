using Newtonsoft.Json.Linq;
using System;

namespace Domains.Ordering.Dtos.Orders
{
    public class OrderDto
    {
        public Guid ProductId { get; set; }
        public Guid ServiceMethodId { get; set; }
        public JObject AdditionalModifiers { get; set; }
    }
}
