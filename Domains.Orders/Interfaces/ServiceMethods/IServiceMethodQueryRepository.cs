using Domains.Orders.QueryModels.ServiceMethods;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domains.Orders.Interfaces.ServiceMethods
{
    public interface IServiceMethodQueryRepository
    {
        Task<IEnumerable<ServiceMethodQueryModel>> GetAllServiceMethods();
    }
}
