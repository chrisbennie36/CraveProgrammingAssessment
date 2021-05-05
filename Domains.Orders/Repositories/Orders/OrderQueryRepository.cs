using Domains.Ordering.Interfaces.Orders;
using Domains.Ordering.QueryModels.Orders;
using Domains.Ordering.Repositories.Orders.Models;
using Infrastructure.Sql.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domains.Ordering.Repositories.Orders
{
    public class OrderQueryRepository : IOrderQueryRepository
    {
        private const string GetOrderProcedure = "spGetOrderById";
        private const string GetOrdersProcedure = "spGetOrdersByFilter";

        private readonly ISqlRepository _sqlRepository;

        public OrderQueryRepository(ISqlRepository sqlRepository)
        {
            _sqlRepository = sqlRepository ?? throw new ArgumentNullException(nameof(sqlRepository));
        }

        public async Task<OrderModel> GetOrderById(Guid orderId)
        {
            var result = await _sqlRepository.QueryAsync<OrderModel>(GetOrderProcedure, new
            {
                orderId = orderId
            }).ConfigureAwait(false);

            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<OrderModel>> GetOrdersByFilter(string filter)
        {
            var result = await _sqlRepository.QueryAsync<OrderModel>(GetOrdersProcedure, new
            {
                filter = filter
            }).ConfigureAwait(false);

            return result ?? new List<OrderModel>();
        }
    }
}
