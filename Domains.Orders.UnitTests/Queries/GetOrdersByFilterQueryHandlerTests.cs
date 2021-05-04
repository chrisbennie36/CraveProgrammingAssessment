using Domains.Orders.Interfaces;
using Domains.Orders.Queries;
using Domains.Orders.QueryModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Domains.Orders.UnitTests.Queries
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

            _orderRepository.Setup(o => o.GetOrdersByFilter(query.Filter)).ReturnsAsync(new List<OrderQueryModel>());

            var result = await _handler.Handle(query, new CancellationToken()).ConfigureAwait(false);

            _orderRepository.Verify(o => o.GetOrdersByFilter(query.Filter), Times.Once);
            Assert.NotNull(result);
        }
    }
}
