using Domains.Orders.Interfaces;
using Domains.Orders.QueryModels;
using Infrastructure.Sql.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domains.Orders.Repositories
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

        public async Task<OrderQueryModel> GetOrderById(Guid orderId)
        {
            var result = await _sqlRepository.QueryAsync<OrderQueryModel>(GetOrderProcedure, new
            {
                orderId = orderId
            }).ConfigureAwait(false);

            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<OrderQueryModel>> GetOrdersByFilter(string filter)
        {
            var result = await _sqlRepository.QueryAsync<OrderQueryModel>(GetOrdersProcedure, new
            {
                filter = filter
            }).ConfigureAwait(false);

            return result ?? new List<OrderQueryModel>();
        }
    }
}
