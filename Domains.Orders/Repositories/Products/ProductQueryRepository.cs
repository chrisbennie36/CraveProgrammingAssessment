using Domains.Ordering.Interfaces.Products;
using Domains.Ordering.QueryModels.Products;
using Domains.Ordering.Repositories.Products.Entities;
using Infrastructure.TableStorage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domains.Ordering.Repositories.Products
{
    public class ProductQueryRepository : IProductQueryRepository
    {
        const string TableReference = "Product";

        private readonly ITableStorageRepository _tableStorageRepository;

        public ProductQueryRepository(ITableStorageRepository tableStoragerRepository)
        {
            _tableStorageRepository = tableStoragerRepository ?? throw new ArgumentNullException(nameof(tableStoragerRepository));
        }

        public async Task<ProductQueryModel> GetProductById(Guid productId)
        {
            var result = await Task.Run(() => _tableStorageRepository.QuerybyIdAsync<ProductEntity>(TableReference, productId)).ConfigureAwait(false);

            if (result != null)
            {
                return new ProductQueryModel
                {
                    Id = result.Id,
                    IsActive = result.IsActive,
                    Name = result.Name,
                    Type = result.Type
                };
            }

            return null;
        }
        public async Task<IEnumerable<ProductQueryModel>> GetAllProducts()
        {
            var result = await Task.Run(() => _tableStorageRepository.QueryAllAsync<ProductEntity>(TableReference)).ConfigureAwait(false);

            if (result != null)
            {
                return result.Select(r => new ProductQueryModel
                {
                    Id = r.Id,
                    IsActive = r.IsActive,
                    Name = r.Name,
                    Type = r.Type
                });
            }

            return new List<ProductQueryModel>();
        }
    }
}
