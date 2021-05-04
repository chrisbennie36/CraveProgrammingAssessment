using Domains.Products.Enums;
using MediatR;
using System;

namespace Domains.Products.Events
{
    public class ProductAddedEvent : INotification
    {
        public Guid Id { get; set; }
        public ProductName Name { get; set; }
        public ProductType Type { get; set; }
        public bool IsActive { get; set; }
    }
}
