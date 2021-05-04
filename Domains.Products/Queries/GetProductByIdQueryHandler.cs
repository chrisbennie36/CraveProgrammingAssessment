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

        public Task<ProductQueryModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var result = _productQueryRepository.GetProductById(request.Id);

            return Task.FromResult(result);
        }
    }
}
