using MediatR;
using System;

namespace Domains.Orders.Commands.ServiceMethods
{
    public class AddServiceMethodCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
