using Domains.Products.Enums;
using MediatR;
using System;

namespace Domains.Products.Commands
{
    public class AddProductCommand : IRequest<Guid>
    {
        public ProductName Name { get; set; }
        public ProductType Type { get; set; }
    }
}
