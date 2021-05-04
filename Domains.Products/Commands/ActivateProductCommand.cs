using MediatR;
using System;

namespace Domains.Products.Commands
{
    public class ActivateProductCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
