using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domains.Ordering.Interfaces.Orders
{
    public interface IProductIdsQueryRepository
    {
        Task<IEnumerable<Guid>> GetAllProductIds();
        Task<IEnumerable<Guid>> GetActiveProductIds();
    }
}
