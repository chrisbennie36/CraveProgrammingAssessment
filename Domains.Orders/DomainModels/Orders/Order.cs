using Domains.Ordering.Events.Orders;
using Infrastructure.Aggregate.AggregateRoot;
using System;

namespace Domains.Ordering.DomainModels.Orders
{
    public class Order : AggregateRoot
    {
        public Order(Guid id, OrderDetails details)
        {
            Id = id;
            Details = details;
        }

        public static Order CreateNew(OrderDetails details)
        {
            var id = Guid.NewGuid();
            var order = new Order(id, details);

            order.RegisterEvent(new OrderAddedEvent
            {
                Id = order.Id,
                ProductId = order.Details.ProductId,
                ServiceMethodId = order.Details.ServiceMethodId,
                CustomerName = order.Details.CustomerName,
                CustomerPhoneNumber = order.Details.CustomerPhoneNumber,
                CustomerEmail = order.Details.CustomerEmail,
                AdditionalModifiers = order.Details.AdditionalModifiers
            });

            return order;
        }

        public Guid Id { get; }
        public OrderDetails Details { get; }
    }
}
