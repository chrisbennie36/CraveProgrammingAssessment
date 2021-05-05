using Domains.Ordering.QueryModels.Orders;
using Domains.Ordering.Repositories.Orders;
using Infrastructure.Sql.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Domains.Ordering.UnitTests.Repositories.Orders
{
    public class OrderQueryRepositoryTests
    {
        private readonly Mock<ISqlRepository> _sqlRepository;

        private OrderQueryRepository _orderRepository;

        public OrderQueryRepositoryTests()
        {
            _sqlRepository = new Mock<ISqlRepository>();

            _orderRepository = new OrderQueryRepository(_sqlRepository.Object);
        }

        [Fact]
        [Trait("TestCategory", "UnitTest")]
        public void OrderQueryRepository_ThrowsArgumentNullException_WithMissingDI()
        {
            Assert.Throws<ArgumentNullException>(() => new OrderCommandRepository(null, null));
        }

        [Fact]
        public async Task OrderQueryRepository_GetOrderById_QueriesWithTheCorrectStoredProcedure()
        {
            await _orderRepository.GetOrderById(Guid.NewGuid()).ConfigureAwait(false);

            _sqlRepository.Verify(r => r.QueryAsync<OrderQueryModel>("spGetOrderById", It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public async Task OrderQueryRepository_GetOrderByFilter_QueriesWithTheCorrectStoredProcedure()
        {
            await _orderRepository.GetOrdersByFilter("Test").ConfigureAwait(false);

            _sqlRepository.Verify(r => r.QueryAsync<IEnumerable<OrderQueryModel>>("spGetOrdersByFilter", It.IsAny<object>()), Times.Once);
        }
    }
}
