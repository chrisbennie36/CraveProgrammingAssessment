using Domains.Orders.DomainModels;
using Domains.Orders.Dtos;
using Domains.Orders.Interfaces;
using Infrastructure.EventPublisher.Interfaces;
using Infrastrucure.Exceptions;
using Logging.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Orders.Commands
{
    public class PlaceOrdersCommandHandler : IRequestHandler<PlaceOrdersCommand, IEnumerable<Guid>>
    {
        private readonly IOrderCommandRepository _orderRepository;
        private readonly ILogger _logger;
        private readonly IEventPublisher<INotification> _eventPublisher;

        public PlaceOrdersCommandHandler(IOrderCommandRepository orderRepository, ILogger logger, IEventPublisher<INotification> eventPublisher)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventPublisher = eventPublisher ?? throw new ArgumentNullException(nameof(eventPublisher));
        }

        public async Task<IEnumerable<Guid>> Handle(PlaceOrdersCommand request, CancellationToken cancellationToken)
        {
            _logger.Debug("Handling PlaceOrdersCommand");

            var orderIds = new List<Guid>();

            if (request.CustomerDetails == null)
            {
                throw new CustomerDetailsMissingException("No Customer details supplied in the PlaceOrdersCommand");
            }

            foreach (var orderDto in request.Orders)
            {
                var order = MapOrder(orderDto, request.CustomerDetails);

                await _orderRepository.Store(order).ConfigureAwait(false);

                foreach (var @event in order.PendingChanges)
                {
                    _eventPublisher.Publish(@event);
                }

                orderIds.Add(order.Id);
            }

            return orderIds;
        }

        private Order MapOrder(OrderDto orderDto, CustomerDetailsDto customerDetailsDto)
        {
            var details = new OrderDetails(
                orderDto.ProductId,
                orderDto.ServiceMethodId,
                customerDetailsDto.Name,
                customerDetailsDto.PhoneNumber,
                customerDetailsDto.Email,
                orderDto.AdditionalInstructions);

            return Order.CreateNew(details);
        }
    }
}
