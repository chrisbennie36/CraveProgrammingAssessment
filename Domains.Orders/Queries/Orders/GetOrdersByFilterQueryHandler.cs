using Domains.Ordering.Interfaces.Orders;
using Domains.Ordering.QueryModels.Orders;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Ordering.Queries.Orders
{
    public class GetOrdersByFilterQueryHandler : IRequestHandler<GetOrdersByFilterQuery, IEnumerable<OrderQueryModel>>
    {
        private IOrderQueryRepository _orderQueryRepository;

        public GetOrdersByFilterQueryHandler(IOrderQueryRepository orderQueryRepository)
        {
            _orderQueryRepository = orderQueryRepository ?? throw new ArgumentNullException(nameof(orderQueryRepository));
        }

        public async Task<IEnumerable<OrderQueryModel>> Handle(GetOrdersByFilterQuery request, CancellationToken cancellationToken)
        {            
            var orders = await _orderQueryRepository.GetOrdersByFilter(request.Filter).ConfigureAwait(false);

            return orders.Select(o => new OrderQueryModel 
            {
                Id = o.Id,
                CustomerEmail = o.CustomerEmail,
                CustomerName = o.CustomerName,
                CustomerPhoneNumber = o.CustomerPhoneNumber,
                Additions = o.Additions,
                PreparationMethod = o.PreparationMethod
            });
        }
    }
}
