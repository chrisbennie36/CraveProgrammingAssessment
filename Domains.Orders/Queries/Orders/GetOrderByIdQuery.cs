using Domains.Ordering.QueryModels.Orders;
using MediatR;
using System;

namespace Domains.Ordering.Queries.Orders
{
    public class GetOrderByIdQuery : IRequest<OrderQueryModel>
    {
        public Guid Id { get; set; }
    }
}
