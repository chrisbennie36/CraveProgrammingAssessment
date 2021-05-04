using MediatR;
using System;

namespace Domains.Products.Events
{
    public class ProductActivatedEvent : INotification
    {
        public Guid Id { get; set; }
    }
}
