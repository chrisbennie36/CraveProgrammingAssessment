using Domains.Orders.DomainModels;
using Domains.Orders.Events;
using System;
using Xunit;

namespace Domains.Orders.UnitTests.DomainModels
{
    public class OrderTests
    {
        private OrderDetails _orderDetails;

        public OrderTests()
        {
            _orderDetails = new OrderDetails(Guid.NewGuid(), Guid.NewGuid(), "UnitTestCustomer", "123456789", "unit@test.com", string.Empty);
        }

        [Fact]
        [Trait("TestCategory", "UnitTest")]
        public void CreateNew_ShouldPopulateId()
        {
            var order = Order.CreateNew(_orderDetails);
            Assert.NotEqual(Guid.Empty, order.Id);
        }

        [Fact]
        [Trait("TestCategory", "UnitTest")]
        public void CreateNew_ShouldOnlyFireOrderAddedEvent()
        {
            var order = Order.CreateNew(_orderDetails);
            Assert.Single(order.PendingChanges);
            Assert.Contains(order.PendingChanges, p => p.GetType() == typeof(OrderAddedEvent));
        }
    }
}
