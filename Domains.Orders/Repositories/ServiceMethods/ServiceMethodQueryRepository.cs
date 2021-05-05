using Domains.Ordering.Interfaces.ServiceMethods;
using Domains.Ordering.QueryModels.ServiceMethods;
using Infrastructure.Sql.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domains.Orders.Repositories.ServiceMethods
{
    public class ServiceMethodQueryRepository : IServiceMethodQueryRepository
    {
        private const string GetServiceMethodByIdProcedure = "spGetServiceMethodById";
        private const string GetServiceMethodsProcedure = "spGetServiceMethods";

        private readonly ISqlRepository _sqlRepository;

        public ServiceMethodQueryRepository(ISqlRepository sqlRepository)
        {
            _sqlRepository = sqlRepository ?? throw new ArgumentNullException(nameof(sqlRepository));
        }

        public async Task<ServiceMethodQueryModel> GetServiceMethodById(Guid id)
        {
            var result = await _sqlRepository.QueryAsync<ServiceMethodQueryModel>(GetServiceMethodByIdProcedure, new
            {
                serviceMethodId = id
            }).ConfigureAwait(false);

            return result.SingleOrDefault();
        }

        public async Task<IEnumerable<ServiceMethodQueryModel>> GetAllServiceMethods()
        {
            var result = await _sqlRepository.QueryAsync<ServiceMethodQueryModel>(GetServiceMethodsProcedure, new
            {
            }).ConfigureAwait(false);

            return result ?? new List<ServiceMethodQueryModel>();
        }
    }
}
