using System;

namespace Domains.Orders.QueryModels
{
    public class OrderQueryModel
    {
        public Guid Id { get; set; }
        public Guid Product { get; set; }
        public Guid ServiceMethod { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerEmail { get; set; }
        public string AdditionalInstructions { get; set; }
    }
}
