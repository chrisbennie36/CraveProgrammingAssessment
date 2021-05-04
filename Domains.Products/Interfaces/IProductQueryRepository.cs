using Domains.Products.QueryModels;
using System;

namespace Domains.Products.Interfaces
{
    public interface IProductQueryRepository
    {
        public ProductQueryModel GetProductById(Guid orderId);
    }
}
