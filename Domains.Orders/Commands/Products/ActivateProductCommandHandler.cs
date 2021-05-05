using Domains.Ordering.Interfaces.Products;
using Infrastructure.EventPublisher.Interfaces;
using Logging.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Ordering.Commands.Products
{
    public class ActivateProductCommandHandler : IRequestHandler<ActivateProductCommand, Unit>
    {
        private IProductCommandRepository _productRepository;
        private ILogger _logger;
        private IEventPublisher<INotification> _eventPublisher;

        public ActivateProductCommandHandler(IProductCommandRepository productRepository, ILogger logger, IEventPublisher<INotification> eventPublisher)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventPublisher = eventPublisher ?? throw new ArgumentNullException(nameof(eventPublisher));
        }

        public async Task<Unit> Handle(ActivateProductCommand request, CancellationToken cancellationToken)
        {
            _logger.Debug($"Handling ActivateProductCommand for Product {request.Id}");

            var product = await _productRepository.GetProductById(request.Id).ConfigureAwait(false);

            if (product != null) 
            {
                product.IsActive = true;
                await _productRepository.Store(product).ConfigureAwait(false);

                foreach (var @event in product.PendingChanges)
                {
                    _eventPublisher.Publish(@event);
                }
            }

            return Unit.Value;
        }
    }
}
