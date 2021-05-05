using Domains.Ordering.QueryModels.Orders;
using MediatR;
using System.Collections.Generic;

namespace Domains.Ordering.Queries.Orders
{
    public class GetOrdersByFilterQuery : IRequest<IEnumerable<OrderQueryModel>>
    {
        public string Filter { get; set; }
    }
}
