using Domains.Ordering.DomainModels.Products;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domains.Ordering.Interfaces.Products
{
    public interface IProductCommandRepository
    {
        Task Store(Product product);
        Task<Product> GetProductById(Guid id);
        Task<IEnumerable<Guid>> GetProductIds();
        Task<IEnumerable<Guid>> GetActiveProductIds();
    }
}
