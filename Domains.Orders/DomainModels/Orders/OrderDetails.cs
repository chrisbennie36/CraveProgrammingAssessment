using Newtonsoft.Json.Linq;
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
            JObject additionalModifiers)
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
        public JObject AdditionalModifiers { get; set; }
    }
}
