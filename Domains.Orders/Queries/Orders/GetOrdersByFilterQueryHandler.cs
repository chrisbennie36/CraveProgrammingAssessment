using Domains.Ordering.Interfaces.Orders;
using Domains.Ordering.Interfaces.Products;
using Domains.Ordering.Interfaces.ServiceMethods;
using Domains.Ordering.QueryModels.Orders;
using Domains.Ordering.QueryModels.Products;
using Domains.Ordering.QueryModels.ServiceMethods;
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
        private IOrderQueryRepository _orderRepository;
        private IProductQueryRepository _productRepository;
        private IServiceMethodQueryRepository _serviceMethodRepository;

        public GetOrdersByFilterQueryHandler(
            IOrderQueryRepository orderRepository, 
            IProductQueryRepository productRepository, 
            IServiceMethodQueryRepository serviceRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _serviceMethodRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));

        }

        public async Task<IEnumerable<OrderQueryModel>> Handle(GetOrdersByFilterQuery request, CancellationToken cancellationToken)
        {            
            var orders = await _orderRepository.GetOrdersByFilter(request.Filter).ConfigureAwait(false);

            return await Task.WhenAll(orders.Select(async o => new OrderQueryModel
            {
                Id = o.Id,
                CustomerEmail = o.CustomerEmail,
                CustomerName = o.CustomerName,
                CustomerPhoneNumber = o.CustomerPhoneNumber,
                Additions = o.Additions,
                PreparationMethod = o.PreparationMethod,
                Product = await GetOrderedProduct(o.ProductId).ConfigureAwait(false),
                ServiceMethod = await GetOrderedServiceMethod(o.ServiceMethodId).ConfigureAwait(false)
            }));
        }

        private async Task<ProductQueryModel> GetOrderedProduct(Guid productId)
        {
            return await _productRepository.GetProductById(productId).ConfigureAwait(false);
        }

        private async Task<ServiceMethodQueryModel> GetOrderedServiceMethod(Guid serviceMethodId)
        {
            return await _serviceMethodRepository.GetServiceMethodById(serviceMethodId).ConfigureAwait(false);
        }
    }
}
