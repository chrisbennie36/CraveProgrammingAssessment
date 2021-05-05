using Domains.Ordering.DomainModels.Orders;
using Domains.Ordering.Events.Orders;
using Domains.Ordering.Repositories.Orders;
using Infrastructure.Sql.Interfaces;
using Logging.Interfaces;
using Moq;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Domains.Ordering.UnitTests.Repositories.Orders
{
    public class OrderCommandRepositoryTests
    {
        private readonly Mock<ISqlRepository> _sqlRepository;
        private readonly Mock<ILogger> _logger;
        private readonly Mock<IDbConnection> _dbConnection;
        private readonly Mock<IDbTransaction> _transaction;

        private OrderCommandRepository _orderRepository;

        public OrderCommandRepositoryTests()
        {
            _sqlRepository = new Mock<ISqlRepository>();
            _logger = new Mock<ILogger>();
            _dbConnection = new Mock<IDbConnection>();
            _transaction = new Mock<IDbTransaction>();

            _dbConnection.Setup(c => c.BeginTransaction(IsolationLevel.ReadCommitted)).Returns(_transaction.Object);
            _sqlRepository.Setup(r => r.CreateConnection()).Returns(_dbConnection.Object);

            _orderRepository = new OrderCommandRepository(_sqlRepository.Object, _logger.Object);
        }

        [Fact]
        [Trait("TestCategory", "UnitTest")]
        public void OrderCommandRepository_ThrowsArgumentNullException_WithMissingDI()
        {
            Assert.Throws<ArgumentNullException>(() => new OrderCommandRepository(null, null));
        }

        [Fact]
        public async Task OrderCommandRepository_StoreOrder_WithPendingOrderAddedEvent_InsertsOrderToRepository()
        {
            var order = Order.CreateNew(new OrderDetails(Guid.NewGuid(), Guid.NewGuid(), "TestCustomer", "TestNumber", "TestEmail", null));

            Assert.Single(order.PendingChanges);
            Assert.True(order.PendingChanges.Single().GetType() == typeof(OrderAddedEvent));

            await _orderRepository.Store(order).ConfigureAwait(false);

            _sqlRepository.Verify(r => r.ExecuteAsync("spAddOrder", It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public async Task OrderCommandRepository_StoreOrder_WithoutPendingOrderAddedEvent_DoesNotInsertOrderToRepository()
        {
            var order = new Order(Guid.NewGuid(), new OrderDetails(Guid.NewGuid(), Guid.NewGuid(), "TestCustomer", "TestNumber", "TestEmail", null));

            Assert.False(order.HasChanges());

            await _orderRepository.Store(order).ConfigureAwait(false);

            _sqlRepository.Verify(r => r.ExecuteAsync("spAddOrder", It.IsAny<object>()), Times.Never);
        }

    }
}
