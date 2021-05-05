using Domains.Orders.QueryModels.ServiceMethods;
using MediatR;
using System.Collections.Generic;

namespace Domains.Orders.Queries.ServiceMethods
{
    public class GetServiceMethodsQuery : IRequest<IEnumerable<ServiceMethodQueryModel>>
    {
    }
}
