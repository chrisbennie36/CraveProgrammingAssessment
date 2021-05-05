using Domains.Products.QueryModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domains.Products.Interfaces
{
    public interface IProductQueryRepository
    {
        Task<ProductQueryModel> GetProductById(Guid orderId);
        Task<IEnumerable<ProductQueryModel>> GetAllProducts();
    }
}
