using Domains.Ordering.DomainModels.Products;
using Domains.Ordering.Events.Products;
using Domains.Ordering.Interfaces.Products;
using Domains.Ordering.Repositories.Products.Entities;
using Infrastructure.TableStorage.Interfaces;
using Logging.Interfaces;
using MediatR;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domains.Ordering.Repositories.Products
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

        public async Task<Product> GetProductById(Guid id)
        {
            var result = await Task.Run(() => _tableStorageRepository.QuerybyIdAsync<ProductEntity>(TableReference, id)).ConfigureAwait(false);

            if (result != null)
            {
                var productDetails = new ProductDetails(result.Name, result.Type);
                return new Product(result.Id, productDetails, result.IsActive);
            }

            return null;
        }

        public async Task<IEnumerable<Guid>> GetActiveProductIds()
        {
            var condition = TableQuery.GenerateFilterConditionForBool("IsActive", QueryComparisons.Equal, true);
            var query = new TableQuery<ProductEntity>().Where(condition);

            var result = await Task.Run(() => _tableStorageRepository.QueryAsync(TableReference, query)).ConfigureAwait(false);

            if (result != null)
            {
                return result.Select(result => result.Id);
            }

            return new List<Guid>();
        }

        public async Task<IEnumerable<Guid>> GetProductIds()
        {
            var result = await Task.Run(() => _tableStorageRepository.QueryAllAsync<ProductEntity>(TableReference)).ConfigureAwait(false);

            if (result != null)
            {
                return result.Select(result => result.Id);
            }

            return new List<Guid>();
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
                        case ProductActivatedEvent productActivated:
                            await ActivateProduct(productActivated).ConfigureAwait(false);
                            break;
                        case ProductDeactivatedEvent productDeactivated:
                            await DeactivateProduct(productDeactivated).ConfigureAwait(false);
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
                Name = @event.Name.ToString(),
                Type = @event.Type.ToString(),
                PartitionKey = @event.Type.ToString(),
                RowKey = @event.Id.ToString(),
                Timestamp = DateTime.UtcNow,
                IsActive = @event.IsActive
            };

            await _tableStorageRepository.InsertAsync(TableReference, entity).ConfigureAwait(false);
        }

        private async Task ActivateProduct(ProductActivatedEvent @event)
        {
            var entity = new ProductEntity
            {
                Id = @event.Id,
                PartitionKey = @event.Type.ToString(),
                RowKey = @event.Id.ToString(),
                Timestamp = DateTime.UtcNow,
                IsActive = true,
                ETag = "*"
            };

            await _tableStorageRepository.UpdateAsync(TableReference, entity).ConfigureAwait(false);
        }

        private async Task DeactivateProduct(ProductDeactivatedEvent @event)
        {
            var entity = new ProductEntity
            {
                Id = @event.Id,
                PartitionKey = @event.Type.ToString(),
                RowKey = @event.Id.ToString(),
                Timestamp = DateTime.UtcNow,
                IsActive = false,
                ETag = "*"
            };

            await _tableStorageRepository.UpdateAsync(TableReference, entity).ConfigureAwait(false);
        }
    }
}
