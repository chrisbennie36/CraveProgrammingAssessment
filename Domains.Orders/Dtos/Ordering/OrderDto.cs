using Domains.Ordering.Dtos.Ordering;
using System;

namespace Domains.Ordering.Dtos.Orders
{
    public class OrderDto
    {
        public Guid ProductId { get; set; }
        public Guid ServiceMethodId { get; set; }
        public AdditionalModifiersDto AdditionalModifiers { get; set; }
    }
}
