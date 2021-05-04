using System;

namespace Domains.Orders.Dtos
{
    public class OrderDto
    {
        public Guid ProductId { get; set; }
        public Guid ServiceMethodId { get; set; }
        public string AdditionalInstructions { get; set; }
    }
}
