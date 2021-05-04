using MediatR;
using System;

namespace Domains.Products.Commands
{
    public class DeactivateProductCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
