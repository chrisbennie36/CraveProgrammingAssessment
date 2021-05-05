using System;

namespace Domains.Ordering.Repositories.Orders.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid ServiceMethodId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerEmail { get; set; }
        public int Additions { get; set; }
        public string PreparationMethod { get; set; }
    }
}
