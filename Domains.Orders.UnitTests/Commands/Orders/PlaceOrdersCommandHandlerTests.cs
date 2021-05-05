using Domains.Ordering.Commands.Orders;
using Domains.Ordering.Dtos.Orders;
using Domains.Ordering.Events.Orders;
using Domains.Ordering.Interfaces.Orders;
using Infrastructure.EventPublisher.Interfaces;
using Logging.Interfaces;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Domains.Ordering.UnitTests.Commands.Orders
{
    public class PlaceOrdersCommandHandlerTests
    {
        private readonly Mock<IOrderCommandRepository> _ordersRepository;
        private readonly Mock<ILogger> _logger;
        private readonly Mock<IEventPublisher<INotification>> _eventPublisher;

        private PlaceOrdersCommandHandler _handler;

        public PlaceOrdersCommandHandlerTests()
        {
            _ordersRepository = new Mock<IOrderCommandRepository>();
            _logger = new Mock<ILogger>();
            _eventPublisher = new Mock<IEventPublisher<INotification>>();

            _handler = new PlaceOrdersCommandHandler(_ordersRepository.Object, _logger.Object, _eventPublisher.Object);
        }

        [Fact]
        [Trait("TestCategory", "UnitTest")]
        public void PlaceOrdersCommandHandler_ThrowsArgumentNullException_WithMissingDI()
        {
            Assert.Throws<ArgumentNullException>(() => new PlaceOrdersCommandHandler(null, null, null));
        }

        [Fact]
        [Trait("TestCategory", "UnitTest")]
        public async Task HandlingPlaceOrdersCommand_OneOrder_FiresOneOrderAddedEvent()
        {
            var command = new PlaceOrdersCommand
            {
                Orders = new List<OrderDto>
                {
                    new OrderDto
                    {
                        ProductId = Guid.NewGuid(),
                        ServiceMethodId = Guid.NewGuid()
                    }
                },
                CustomerDetails = new CustomerDetailsDto
                {
                    Name = "UnitTestCustomer",
                    PhoneNumber = "123456789",
                    Email = "unit@test.com"
                }
            };

            await _handler.Handle(command, new CancellationToken()).ConfigureAwait(false);

            _eventPublisher.Verify(e => e.Publish(It.IsAny<OrderAddedEvent>()), Times.Once);
        }

        [Fact]
        [Trait("TestCategory", "UnitTest")]
        public async Task HandlingPlaceOrdersCommand_TwoOrders_FiresTwoOrderAddedEvents()
        {
            var command = new PlaceOrdersCommand
            {
                Orders = new List<OrderDto>
                {
                    new OrderDto
                    {
                        ProductId = Guid.NewGuid(),
                        ServiceMethodId = Guid.NewGuid()
                    },
                    new OrderDto
                    {
                        ProductId = Guid.NewGuid(),
                        ServiceMethodId = Guid.NewGuid()
                    }
                },
                CustomerDetails = new CustomerDetailsDto
                {
                    Name = "UnitTestCustomer",
                    PhoneNumber = "123456789",
                    Email = "unit@test.com"
                }
            };

            await _handler.Handle(command, new CancellationToken()).ConfigureAwait(false);

            _eventPublisher.Verify(e => e.Publish(It.IsAny<OrderAddedEvent>()), Times.Exactly(2));
        }
    }
}
