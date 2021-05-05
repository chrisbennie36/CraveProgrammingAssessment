using Domains.Ordering.Repositories.Orders.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domains.Ordering.Interfaces.Orders
{
    public interface IOrderQueryRepository
    {
        public Task<OrderModel> GetOrderById(Guid orderId);
        public Task<IEnumerable<OrderModel>> GetOrdersByFilter(string filter); 
    }
}
