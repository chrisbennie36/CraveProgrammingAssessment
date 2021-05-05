using Domains.Ordering.QueryModels.Products;
using MediatR;
using System.Collections.Generic;

namespace Domains.Ordering.Queries.Products
{
    public class GetProductsQuery : IRequest<IEnumerable<ProductQueryModel>>
    {
    }
}
