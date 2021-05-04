using Domains.Orders.Interfaces;
using Domains.Orders.QueryModels;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Orders.Queries
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderQueryModel>
    {
        private readonly IOrderQueryRepository _orderQueryRepository;

        public GetOrderByIdQueryHandler(IOrderQueryRepository orderQueryRepository)
        {
            _orderQueryRepository = orderQueryRepository ?? throw new ArgumentNullException(nameof(orderQueryRepository));
        }

        public async Task<OrderQueryModel> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderQueryRepository.GetOrderById(request.Id).ConfigureAwait(false);

            return order;
        }
    }
}
