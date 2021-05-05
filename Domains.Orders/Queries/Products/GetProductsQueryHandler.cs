using Domains.Products.Interfaces;
using Domains.Products.QueryModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Orders.Queries.Products
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductQueryModel>>
    {
        private readonly IProductQueryRepository _productQueryRepository;

        public GetProductsQueryHandler(IProductQueryRepository productQueryRepository)
        {
            _productQueryRepository = productQueryRepository ?? throw new ArgumentNullException(nameof(productQueryRepository));
        }

        public async Task<IEnumerable<ProductQueryModel>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productQueryRepository.GetAllProducts().ConfigureAwait(false);

            return products;
        }
    }
}
