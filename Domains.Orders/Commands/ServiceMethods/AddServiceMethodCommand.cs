using MediatR;
using System;

namespace Domains.Ordering.Commands.ServiceMethods
{
    public class AddServiceMethodCommand : IRequest<Guid>
    {
        public string Name { get; set; }
    }
}
