using Domains.Products.DomainModels;
using Domains.Products.Interfaces;
using Infrastructure.EventPublisher.Interfaces;
using Logging.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Products.Commands
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Guid>
    {
        private readonly IProductCommandRepository _productRepository;
        private readonly ILogger _logger;
        private readonly IEventPublisher<INotification> _eventPublisher;

        public AddProductCommandHandler(IProductCommandRepository productRepository, ILogger logger, IEventPublisher<INotification> eventPublisher)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventPublisher = eventPublisher ?? throw new ArgumentNullException(nameof(eventPublisher));
        }

        public async Task<Guid> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            _logger.Debug("Handling AddProductCommand");

            var product = MapProduct(request);

            await _productRepository.Store(product).ConfigureAwait(false);

            foreach (var @event in product.PendingChanges)
            {
                _eventPublisher.Publish(@event);
            }

            return product.Id;
        }

        private Product MapProduct(AddProductCommand command)
        {
            var details = new ProductDetails(command.Name, command.Type);

            return Product.CreateNew(details, true);
        }
    }
}
