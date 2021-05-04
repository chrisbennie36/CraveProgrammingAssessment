using Domains.Orders.Interfaces;
using Domains.Orders.Repositories.Entities;
using Infrastructure.TableStorage.Interfaces;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domains.Orders.Repositories
{
    public class ProductIdsQueryRepository : IProductIdsQueryRepository
    {
        const string TableReference = "Product";

        private readonly ITableStorageRepository _tableStorageRepository;

        public ProductIdsQueryRepository(ITableStorageRepository tableStoragerRepository)
        {
            _tableStorageRepository = tableStoragerRepository ?? throw new ArgumentNullException(nameof(tableStoragerRepository));
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

        public async Task<IEnumerable<Guid>> GetAllProductIds()
        {
            var result = await Task.Run(() => _tableStorageRepository.QueryAllAsync<ProductEntity>(TableReference)).ConfigureAwait(false);

            if (result != null)
            {
                return result.Select(result => result.Id);
            }

            return new List<Guid>();
        }
    }
}
