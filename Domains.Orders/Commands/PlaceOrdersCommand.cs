using Domains.Orders.Dtos;
using MediatR;
using System;
using System.Collections.Generic;

namespace Domains.Orders.Commands
{
    public class PlaceOrdersCommand : IRequest<IEnumerable<Guid>>
    {
        public IEnumerable<OrderDto> Orders { get; set; } = new List<OrderDto>();
        public CustomerDetailsDto CustomerDetails { get; set; }

    }
}
