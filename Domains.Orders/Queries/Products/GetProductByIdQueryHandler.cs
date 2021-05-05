using Domains.Products.Interfaces;
using Domains.Products.QueryModels;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Products.Queries
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
