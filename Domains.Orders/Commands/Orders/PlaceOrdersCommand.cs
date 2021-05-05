using Domains.Ordering.Dtos.Orders;
using MediatR;
using System;
using System.Collections.Generic;

namespace Domains.Ordering.Commands.Orders
{
    public class PlaceOrdersCommand : IRequest<IEnumerable<Guid>>
    {
        public IEnumerable<OrderDto> Orders { get; set; } = new List<OrderDto>();
        public CustomerDetailsDto CustomerDetails { get; set; }

    }
}
