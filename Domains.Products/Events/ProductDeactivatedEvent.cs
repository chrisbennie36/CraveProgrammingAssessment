using Domains.Products.Enums;
using MediatR;
using System;

namespace Domains.Products.Events
{
    public class ProductDeactivatedEvent : INotification
    {
        public Guid Id { get; set; }
        public ProductType Type { get; set; }
    }
}
