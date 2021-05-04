using Domains.Orders.QueryModels;
using MediatR;
using System;

namespace Domains.Orders.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderQueryModel>
    {
        public Guid Id { get; set; }
    }
}
