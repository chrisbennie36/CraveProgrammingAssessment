using Domains.Ordering.Dtos.Ordering;
using System;

namespace Domains.Ordering.DomainModels.Orders
{
    public class OrderDetails
    {
        public OrderDetails(
            Guid productId,
            Guid serviceMethodId,
            string customerName,
            string customerPhoneNumber,
            string customerEmail,
            AdditionalModifiersDto additionalModifiers)
        {
            ProductId = productId;
            CustomerName = customerName;
            CustomerPhoneNumber = customerPhoneNumber;
            CustomerEmail = customerEmail;
            ServiceMethodId = serviceMethodId;
            AdditionalModifiers = additionalModifiers;
        }

        public Guid ProductId { get; }
        public Guid ServiceMethodId { get; }
        public string CustomerName { get; }
        public string CustomerPhoneNumber { get; }
        public string CustomerEmail { get; }
        public AdditionalModifiersDto AdditionalModifiers { get; set; }
    }
}
