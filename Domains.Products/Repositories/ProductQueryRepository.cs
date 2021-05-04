using Domains.Products.Interfaces;
using Domains.Products.QueryModels;
using Domains.Products.Repositories.Entities;
using Infrastructure.TableStorage.Interfaces;
using System;

namespace Domains.Products.Repositories
{
    public class ProductQueryRepository : IProductQueryRepository
    {
        const string TableReference = "Product";

        private readonly ITableStorageRepository _tableStorageRepository;

        public ProductQueryRepository(ITableStorageRepository tableStoragerRepository)
        {
            _tableStorageRepository = tableStoragerRepository ?? throw new ArgumentNullException(nameof(tableStoragerRepository));
        }

        public ProductQueryModel GetProductById(Guid productId)
        {
            var result = _tableStorageRepository.QuerybyId<ProductEntity>(TableReference, productId);

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
    }
}
