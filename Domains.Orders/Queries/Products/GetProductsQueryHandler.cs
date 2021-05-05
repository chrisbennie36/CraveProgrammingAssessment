using Domains.Ordering.Interfaces.Products;
using Domains.Ordering.QueryModels.Products;
using Logging.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Ordering.Queries.Products
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductQueryModel>>
    {
        private readonly IProductQueryRepository _productQueryRepository;
        private readonly ILogger _logger;

        public GetProductsQueryHandler(IProductQueryRepository productQueryRepository, ILogger logger)
        {
            _productQueryRepository = productQueryRepository ?? throw new ArgumentNullException(nameof(productQueryRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<ProductQueryModel>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            _logger.Debug("Handling GetProductsQuery");

            var products = await _productQueryRepository.GetAllProducts().ConfigureAwait(false);

            return products;
        }
    }
}
