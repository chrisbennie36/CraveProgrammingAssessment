using Domains.Ordering.Interfaces.Orders;
using Domains.Ordering.Queries.Orders;
using Domains.Ordering.QueryModels.Orders;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Domains.Ordering.UnitTests.Queries.Orders
{
    public class GetOrderByIdQueryHandlerTests
    {
        private Mock<IOrderQueryRepository> _orderRepository;

        private GetOrderByIdQueryHandler _handler;

        public GetOrderByIdQueryHandlerTests()
        {
            _orderRepository = new Mock<IOrderQueryRepository>();

            _handler = new GetOrderByIdQueryHandler(_orderRepository.Object);
        }

        [Fact]
        [Trait("TestCategory", "UnitTest")]
        public void GetOrderByIdQueryHandler_ThrowsArgumentNullException_WithMissingReferences()
        {
            Assert.Throws<ArgumentNullException>(() => new GetOrderByIdQueryHandler(null));
        }


        [Fact]
        [Trait("TestCategory", "UnitTest")]
        public async Task HandlingGetOrderByIdQuery_RetrievesDataFromTheRepository()
        {
            var query = new GetOrderByIdQuery { Id = Guid.NewGuid() };

            _orderRepository.Setup(o => o.GetOrderById(query.Id)).ReturnsAsync(new OrderQueryModel());

            var result = await _handler.Handle(query, new CancellationToken()).ConfigureAwait(false);

            _orderRepository.Verify(o => o.GetOrderById(query.Id), Times.Once);
            Assert.NotNull(result);
        }
    }
}
