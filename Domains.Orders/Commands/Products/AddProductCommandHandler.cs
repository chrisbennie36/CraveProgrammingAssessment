using Domains.Ordering.DomainModels.Products;
using Domains.Ordering.Interfaces.Products;
using Infrastructure.EventPublisher.Interfaces;
using Logging.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Ordering.Commands.Products
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
            var details = new ProductDetails(command.Name.ToString(), command.Type.ToString());

            return Product.CreateNew(details, true);
        }
    }
}
