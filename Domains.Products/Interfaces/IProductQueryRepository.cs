using Domains.Products.QueryModels;
using System;
using System.Threading.Tasks;

namespace Domains.Products.Interfaces
{
    public interface IProductQueryRepository
    {
        public Task<ProductQueryModel> GetProductById(Guid orderId);
    }
}
