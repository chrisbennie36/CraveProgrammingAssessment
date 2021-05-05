using Domains.Ordering.QueryModels.ServiceMethods;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domains.Ordering.Interfaces.ServiceMethods
{
    public interface IServiceMethodQueryRepository
    {
        Task<IEnumerable<ServiceMethodQueryModel>> GetAllServiceMethods();
    }
}
