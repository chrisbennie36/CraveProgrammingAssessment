using Domains.Products.DomainModels;
using Domains.Products.Events;
using Domains.Products.Interfaces;
using Domains.Products.Repositories.Entities;
using Infrastructure.TableStorage.Interfaces;
using Logging.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domains.Products.Repositories
{
    public class ProductCommandRepository : IProductCommandRepository
    {
        const string TableReference = "Product";

        private readonly ITableStorageRepository _tableStorageRepository;
        private readonly ILogger _logger;

        public ProductCommandRepository(ITableStorageRepository tableStorageRepository, ILogger logger)
        {
            _tableStorageRepository = tableStorageRepository ?? throw new ArgumentNullException(nameof(tableStorageRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Store(Product product)
        {
            if (!product.HasChanges())
            {
                return;
            }

            await DispatchEvents(product.PendingChanges).ConfigureAwait(false);
        }

        private async Task DispatchEvents(IReadOnlyList<INotification> events)
        {
            try
            {
                foreach (var @event in events)
                {
                    switch (@event)
                    {
                        case ProductAddedEvent productAdded:
                            await AddProduct(productAdded).ConfigureAwait(false);
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error(e);

                throw;
            }
        }

        private async Task AddProduct(ProductAddedEvent @event)
        {
            var entity = new ProductEntity
            {
                Id = @event.Id,
                Name = @event.Name,
                Type = @event.Type,
                PartitionKey = @event.Type.ToString(),
                RowKey = @event.Id.ToString(),
                Timestamp = DateTime.UtcNow,
                IsActive = @event.IsActive
            };

            await _tableStorageRepository.ExecuteAsync(TableReference, entity).ConfigureAwait(false);
        }
    }
}
