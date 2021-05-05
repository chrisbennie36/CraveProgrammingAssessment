using Domains.Ordering.Interfaces.Products;
using Domains.Ordering.QueryModels.Products;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Ordering.Queries.Products
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductQueryModel>
    {
        private IProductQueryRepository _productQueryRepository;

        public GetProductByIdQueryHandler(IProductQueryRepository productQueryRepository)
        {
            _productQueryRepository = productQueryRepository ?? throw new ArgumentNullException(nameof(productQueryRepository));
        }

        public async Task<ProductQueryModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _productQueryRepository.GetProductById(request.Id).ConfigureAwait(false);

            return result;
        }
    }
}
