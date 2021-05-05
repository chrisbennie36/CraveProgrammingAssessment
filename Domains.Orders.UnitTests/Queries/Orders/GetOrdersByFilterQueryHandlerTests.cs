using Domains.Ordering.Interfaces.Orders;
using Domains.Ordering.Queries.Orders;
using Domains.Ordering.Repositories.Orders.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Domains.Ordering.UnitTests.Queries.Orders
{
    public class GetOrdersByFilterQueryHandlerTests
    {
        private Mock<IOrderQueryRepository> _orderRepository;

        private GetOrdersByFilterQueryHandler _handler;

        public GetOrdersByFilterQueryHandlerTests()
        {
            _orderRepository = new Mock<IOrderQueryRepository>();

            _handler = new GetOrdersByFilterQueryHandler(_orderRepository.Object);
        }

        [Fact]
        [Trait("TestCategory", "UnitTest")]
        public void GetOrderByIdQueryHandler_ThrowsArgumentNullException_WithMissingReferences()
        {
            Assert.Throws<ArgumentNullException>(() => new GetOrdersByFilterQueryHandler(null));
        }

        [Fact]
        [Trait("TestCategory", "UnitTest")]
        public async Task HandlingGetOrdersByFilterQuery_RetrievesDataFromTheRepository()
        {
            var query = new GetOrdersByFilterQuery { Filter = "UnitTest" };

            _orderRepository.Setup(o => o.GetOrdersByFilter(query.Filter)).ReturnsAsync(new List<OrderModel>());

            var result = await _handler.Handle(query, new CancellationToken()).ConfigureAwait(false);

            _orderRepository.Verify(o => o.GetOrdersByFilter(query.Filter), Times.Once);
            Assert.NotNull(result);
        }
    }
}
