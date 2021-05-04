using Domains.Orders.QueryModels;
using MediatR;

namespace Domains.Orders.Queries
{
    public class GetOrdersByFilterQuery : IRequest<OrdersQueryModel>
    {
        public string Filter { get; set; }
    }
}
