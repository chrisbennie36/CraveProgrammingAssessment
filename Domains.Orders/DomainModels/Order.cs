using Domains.Orders.Events;
using Infrastructure.Aggregate.AggregateRoot;
using System;

namespace Domains.Orders.DomainModels
{
    public class Order : AggregateRoot
    {
        public Order(OrderDetails details)
        {
            Id = Guid.NewGuid();
            Details = details;
        }

        public static Order CreateNew(OrderDetails details)
        {
            var order = new Order(details);

            order.RegisterEvent(new OrderAddedEvent
            {
                Id = order.Id,
                ProductId = order.Details.ProductId,
                ServiceMethodId = order.Details.ServiceMethodId,
                CustomerName = order.Details.CustomerName,
                CustomerPhoneNumber = order.Details.CustomerPhoneNumber,
                CustomerEmail = order.Details.CustomerEmail,
                AdditionalInstructions = order.Details.AdditionalInstructions
            });

            return order;
        }

        public Guid Id { get; }
        public OrderDetails Details { get; }
    }
}
