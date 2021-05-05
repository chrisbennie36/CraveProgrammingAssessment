using Domains.Ordering.QueryModels.Products;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domains.Ordering.Interfaces.Products
{
    public interface IProductQueryRepository
    {
        Task<ProductQueryModel> GetProductById(Guid orderId);
        Task<IEnumerable<ProductQueryModel>> GetAllProducts();
    }
}
