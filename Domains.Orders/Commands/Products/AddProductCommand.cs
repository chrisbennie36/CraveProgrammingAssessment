using Domains.Ordering.Enums.Products;
using MediatR;
using System;

namespace Domains.Ordering.Commands.Products
{
    public class AddProductCommand : IRequest<Guid>
    {
        public ProductName Name { get; set; }
        public ProductType Type { get; set; }
    }
}
