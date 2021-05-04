using Domains.Orders.QueryModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domains.Orders.Interfaces
{
    public interface IOrderQueryRepository
    {
        public Task<OrderQueryModel> GetOrderById(Guid orderId);
        public Task<IEnumerable<OrderQueryModel>> GetOrdersByFilter(string filter); 
    }
}
