using Domains.Orders.DomainModels.ServiceMethods;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domains.Orders.Interfaces.ServiceMethods
{
    public interface IServiceMethodCommandRepository
    {
        Task Store(ServiceMethod serviceMethod);
        Task<IEnumerable<Guid>> GetServiceMethodIds();

    }
}
