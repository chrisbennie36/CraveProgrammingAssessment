using MediatR;
using System;

namespace Domains.Ordering.Commands.Products
{
    public class DeactivateProductCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
