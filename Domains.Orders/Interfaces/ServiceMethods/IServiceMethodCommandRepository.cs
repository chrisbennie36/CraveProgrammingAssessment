using Domains.Ordering.DomainModels.ServiceMethods;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domains.Ordering.Interfaces.ServiceMethods
{
    public interface IServiceMethodCommandRepository
    {
        Task Store(ServiceMethod serviceMethod);
        Task<IEnumerable<Guid>> GetServiceMethodIds();

    }
}
