using System.Collections.Generic;

namespace Domains.Orders.QueryModels
{
    public class OrdersQueryModel
    {
        public IEnumerable<OrderQueryModel> Orders { get; set; } = new List<OrderQueryModel>();
    }
}
