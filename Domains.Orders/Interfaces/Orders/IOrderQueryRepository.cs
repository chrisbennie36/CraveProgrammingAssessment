using Domains.Ordering.QueryModels.Orders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domains.Ordering.Interfaces.Orders
{
    public interface IOrderQueryRepository
    {
        public Task<OrderQueryModel> GetOrderById(Guid orderId);
        public Task<IEnumerable<OrderQueryModel>> GetOrdersByFilter(string filter); 
    }
}
