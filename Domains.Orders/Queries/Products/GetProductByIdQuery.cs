using Domains.Ordering.QueryModels.Products;
using MediatR;
using System;

namespace Domains.Ordering.Queries.Products
{
    public class GetProductByIdQuery : IRequest<ProductQueryModel>
    {
        public Guid Id { get; set; }
    }
}
