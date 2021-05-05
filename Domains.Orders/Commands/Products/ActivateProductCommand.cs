using MediatR;
using System;

namespace Domains.Ordering.Commands.Products
{
    public class ActivateProductCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
