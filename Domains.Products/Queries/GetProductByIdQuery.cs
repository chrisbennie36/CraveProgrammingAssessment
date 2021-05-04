using Domains.Products.QueryModels;
using MediatR;
using System;

namespace Domains.Products.Queries
{
    public class GetProductByIdQuery : IRequest<ProductQueryModel>
    {
        public Guid Id { get; set; }
    }
}
