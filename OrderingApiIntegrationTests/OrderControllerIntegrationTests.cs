using Domains.Orders.Commands;
using Domains.Orders.Dtos;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using Domains.Orders.Queries;
using Domains.Orders.QueryModels;

namespace OrderingApiIntegrationTests
{
    public class OrderControllerIntegrationTests
    {
        private string baseUrl = "/api/order";

        private const string CustomerName = "IntegrationTestCustomer";

        //ToDo: Take from database once populated
        private Guid _productId = Guid.NewGuid();

        [Fact]
        [Trait("TestCategory", "IntegrationTests")]
        public void AddOrder()
        {
            var placeOrdersCommand = new PlaceOrdersCommand
            {
                CustomerDetails = new CustomerDetailsDto
                {
                    Name = CustomerName
                    Email = "integration@test.com",
                    PhoneNumber = "123456789"
                },
                Orders = new List<OrderDto>
                {
                    new OrderDto
                    {
                        ProductId = Guid.NewGuid()
                    }
                }
            };

            var orderIds = GetPostPutDelete.Post<PlaceOrdersCommand, Guid>(baseUrl, placeOrdersCommand);

            Assert.NotNull(orderIds);
            Assert.NotEmpty(orderIds);
            Assert.Equal(1, orderIds.Count());
        }

        [Fact]
        [Trait("TestCategory", "IntegrationTests")]
        public void RetrieveOrder()
        {
            var placeOrdersCommand = new PlaceOrdersCommand
            {
                CustomerDetails = new CustomerDetailsDto
                {
                    Name = CustomerName
                    Email = "integration@test.com",
                    PhoneNumber = "123456789"
                },
                Orders = new List<OrderDto>
                {
                    new OrderDto
                    {
                        ProductId = Guid.NewGuid()
                    }
                }
            };

            var orderIds = GetPostPutDelete.Post<PlaceOrdersCommand, Guid>(baseUrl, placeOrdersCommand);

            Assert.NotNull(orderIds);
            Assert.NotEmpty(orderIds);
            Assert.Equal(1, orderIds.Count());


            var order = GetPostPutDelete.Get(baseUrl + "/" + orderIds.First());

            Assert.NotNull(order);
            Assert.Equal(orderIds.First(), order.Id);
        }

        [Fact]
        [Trait("TestCategory", "IntegrationTests")]
        public void RetrieveOrders()
        {
            var getOrdersQuery = new GetOrdersByFilterQuery
            {
                Filter = CustomerName
            };

            var orders = GetPostPutDelete.Post<GetOrdersByFilterQuery, OrdersQueryModel>(baseUrl, getOrdersQuery);

            Assert.NotNull(orders);
            Assert.NotNull(orders.Orders);
            Assert.NotEmpty(orders.Orders);
        }
    }
}
