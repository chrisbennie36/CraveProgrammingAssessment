using Domains.Ordering.Interfaces.Orders;
using Domains.Ordering.Interfaces.Products;
using Domains.Ordering.Interfaces.ServiceMethods;
using Domains.Ordering.Queries.Orders;
using Domains.Ordering.Repositories.Orders.Models;
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
        private Mock<IProductQueryRepository> _productRepository;
        private Mock<IServiceMethodQueryRepository> _serviceMethodRepository;

        private GetOrderByIdQueryHandler _handler;

        public GetOrderByIdQueryHandlerTests()
        {
            _orderRepository = new Mock<IOrderQueryRepository>();
            _productRepository = new Mock<IProductQueryRepository>();
            _serviceMethodRepository = new Mock<IServiceMethodQueryRepository>();

            _handler = new GetOrderByIdQueryHandler(_orderRepository.Object, _productRepository.Object, _serviceMethodRepository.Object);
        }

        [Fact]
        [Trait("TestCategory", "UnitTest")]
        public void GetOrderByIdQueryHandler_ThrowsArgumentNullException_WithMissingReferences()
        {
            Assert.Throws<ArgumentNullException>(() => new GetOrderByIdQueryHandler(null, null, null));
        }


        [Fact]
        [Trait("TestCategory", "UnitTest")]
        public async Task HandlingGetOrderByIdQuery_RetrievesOrderData()
        {
            var query = new GetOrderByIdQuery { Id = Guid.NewGuid() };

            _orderRepository.Setup(o => o.GetOrderById(query.Id)).ReturnsAsync(new OrderModel());

            var result = await _handler.Handle(query, new CancellationToken()).ConfigureAwait(false);

            _orderRepository.Verify(o => o.GetOrderById(query.Id), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        [Trait("TestCategory", "UnitTest")]
        public async Task HandlingGetOrderByIdQuery_RetrievesProductData()
        {
            var productId = Guid.NewGuid();

            var query = new GetOrderByIdQuery { Id = Guid.NewGuid() };

            _orderRepository.Setup(o => o.GetOrderById(query.Id)).ReturnsAsync(new OrderModel { ProductId = productId });

            var result = await _handler.Handle(query, new CancellationToken()).ConfigureAwait(false);

            _productRepository.Verify(o => o.GetProductById(productId), Times.Once);
        }

        [Fact]
        [Trait("TestCategory", "UnitTest")]
        public async Task HandlingGetOrderByIdQuery_RetrievesServiceMethodData()
        {
            var serviceMethodId = Guid.NewGuid();

            var query = new GetOrderByIdQuery { Id = Guid.NewGuid() };

            _orderRepository.Setup(o => o.GetOrderById(query.Id)).ReturnsAsync(new OrderModel { ServiceMethodId = serviceMethodId });

            var result = await _handler.Handle(query, new CancellationToken()).ConfigureAwait(false);

            _serviceMethodRepository.Verify(o => o.GetServiceMethodById(serviceMethodId), Times.Once);
        }
    }
}
