using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domains.Orders.Interfaces
{
    public interface IProductIdsQueryRepository
    {
        Task<IEnumerable<Guid>> GetProductIds();
    }
}
