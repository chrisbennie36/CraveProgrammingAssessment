using System;

namespace Domains.Orders.DomainModels
{
    public class OrderDetails
    {
        public OrderDetails(
            Guid productId,
            Guid serviceMethodId,
            string customerName, 
            string customerPhoneNumber, 
            string customerEmail,
            string additionalInstructions)
        {
            ProductId = productId;
            CustomerName = customerName;
            CustomerPhoneNumber = customerPhoneNumber;
            CustomerEmail = customerEmail;
            ServiceMethodId = serviceMethodId;
            AdditionalInstructions = additionalInstructions;
        }

        public Guid ProductId { get; }
        public Guid ServiceMethodId { get; }
        public string CustomerName{ get; }
        public string CustomerPhoneNumber { get; }
        public string CustomerEmail { get; }
        public string AdditionalInstructions { get; set; }
    }
}
