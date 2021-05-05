using Domains.Orders.Interfaces.ServiceMethods;
using Domains.Orders.QueryModels.ServiceMethods;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Orders.Queries.ServiceMethods
{
    public class GetServiceMethodsQueryHandler : IRequestHandler<GetServiceMethodsQuery, IEnumerable<ServiceMethodQueryModel>>
    {
        private readonly IServiceMethodQueryRepository _serviceMethodQueryRepository;

        public GetServiceMethodsQueryHandler(IServiceMethodQueryRepository serviceMethodQueryRepository)
        {
            _serviceMethodQueryRepository = serviceMethodQueryRepository ?? throw new ArgumentNullException(nameof(serviceMethodQueryRepository));
        }

        public async Task<IEnumerable<ServiceMethodQueryModel>> Handle(GetServiceMethodsQuery request, CancellationToken cancellationToken)
        {
            var serviceMethods = await _serviceMethodQueryRepository.GetAllServiceMethods().ConfigureAwait(false);

            return serviceMethods;
        }
    }
}
