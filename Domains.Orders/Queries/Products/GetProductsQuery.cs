using Domains.Products.QueryModels;
using MediatR;
using System.Collections.Generic;

namespace Domains.Orders.Queries.Products
{
    public class GetProductsQuery : IRequest<IEnumerable<ProductQueryModel>>
    {
    }
}
