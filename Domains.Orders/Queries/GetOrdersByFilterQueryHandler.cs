using Domains.Orders.Interfaces;
using Domains.Orders.QueryModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Orders.Queries
{
    public class GetOrdersByFilterQueryHandler : IRequestHandler<GetOrdersByFilterQuery, OrdersQueryModel>
    {
        private IOrderQueryRepository _orderQueryRepository;

        public GetOrdersByFilterQueryHandler(IOrderQueryRepository orderQueryRepository)
        {
            _orderQueryRepository = orderQueryRepository ?? throw new ArgumentNullException(nameof(orderQueryRepository));
        }

        public async Task<OrdersQueryModel> Handle(GetOrdersByFilterQuery request, CancellationToken cancellationToken)
        {            
            var orders = await _orderQueryRepository.GetOrdersByFilter(request.Filter).ConfigureAwait(false);

            return new OrdersQueryModel
            {
                Orders = orders ?? new List<OrderQueryModel>()
            };
        }
    }
}
