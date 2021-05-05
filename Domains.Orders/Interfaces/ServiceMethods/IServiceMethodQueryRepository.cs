using Domains.Ordering.QueryModels.ServiceMethods;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domains.Ordering.Interfaces.ServiceMethods
{
    public interface IServiceMethodQueryRepository
    {
        Task<ServiceMethodQueryModel> GetServiceMethodById(Guid id);
        Task<IEnumerable<ServiceMethodQueryModel>> GetAllServiceMethods();

    }
}
