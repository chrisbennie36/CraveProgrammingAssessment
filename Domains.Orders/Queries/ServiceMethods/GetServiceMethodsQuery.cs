using Domains.Ordering.QueryModels.ServiceMethods;
using MediatR;
using System.Collections.Generic;

namespace Domains.Ordering.Queries.ServiceMethods
{
    public class GetServiceMethodsQuery : IRequest<IEnumerable<ServiceMethodQueryModel>>
    {
    }
}
