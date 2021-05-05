using Domains.Ordering.Interfaces.Orders;
using Domains.Ordering.Interfaces.Products;
using Domains.Ordering.Interfaces.ServiceMethods;
using Domains.Ordering.QueryModels.Orders;
using Domains.Ordering.QueryModels.Products;
using Domains.Ordering.QueryModels.ServiceMethods;
using Domains.Ordering.Repositories.Orders.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Ordering.Queries.Orders
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderQueryModel>
    {
        private readonly IOrderQueryRepository _orderRepository;
        private readonly IProductQueryRepository _productRepository;
        private readonly IServiceMethodQueryRepository _serviceMethodRepository;

        public GetOrderByIdQueryHandler(IOrderQueryRepository orderQueryRepository, IProductQueryRepository productRepository, IServiceMethodQueryRepository serviceMethodQueryRepository)
        {
            _orderRepository = orderQueryRepository ?? throw new ArgumentNullException(nameof(orderQueryRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _serviceMethodRepository = serviceMethodQueryRepository ?? throw new ArgumentNullException(nameof(serviceMethodQueryRepository));
        }

        public async Task<OrderQueryModel> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderById(request.Id).ConfigureAwait(false);

            var orderedProduct = await _productRepository.GetProductById(order.ProductId).ConfigureAwait(false);
            var orderedServiceMethod = await _serviceMethodRepository.GetServiceMethodById(order.ServiceMethodId).ConfigureAwait(false);

            return MapQueryModel(order, orderedProduct, orderedServiceMethod);
        }

        private OrderQueryModel MapQueryModel(OrderModel order, ProductQueryModel product, ServiceMethodQueryModel serviceMethod)
        {
            return new OrderQueryModel
            {
                Id = order.Id,
                Additions = order.Additions,
                PreparationMethod = order.PreparationMethod,
                CustomerEmail = order.CustomerEmail,
                CustomerName = order.CustomerName,
                CustomerPhoneNumber = order.CustomerPhoneNumber,
                Product = product,
                ServiceMethod = serviceMethod
            };
        }
    }
}
