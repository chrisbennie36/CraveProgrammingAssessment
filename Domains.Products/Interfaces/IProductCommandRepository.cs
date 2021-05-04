using Domains.Products.DomainModels;
using System;
using System.Threading.Tasks;

namespace Domains.Products.Interfaces
{
    public interface IProductCommandRepository
    {
        Task Store(Product product);
        Task<Product> GetProductById(Guid id);
    }
}
