using Domains.Ordering.DomainModels.ServiceMethods;
using Domains.Ordering.Events.ServiceMethods;
using Domains.Ordering.Interfaces.ServiceMethods;
using Infrastructure.Sql.Interfaces;
using Logging.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Domains.Ordering.Repositories.ServiceMethods
{
    public class ServiceMethodCommandRepository : IServiceMethodCommandRepository
    {
        private const string AddServiceMethodProcedure = "spAddServiceMethod";
        private const string GetServiceMethodIdsProcedure = "spGetServiceMethodIds";

        private IDbConnection _connection = null;
        private IDbTransaction _transaction = null;

        private readonly ISqlRepository _sqlRepository;
        private readonly ILogger _logger;

        public ServiceMethodCommandRepository(ISqlRepository sqlRepository, ILogger logger)
        {
            _sqlRepository = sqlRepository ?? throw new ArgumentNullException(nameof(sqlRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Store(ServiceMethod serviceMethod)
        {
            if (!serviceMethod.HasChanges())
            {
                return;
            }

            await DispatchEvents(serviceMethod.PendingChanges).ConfigureAwait(false);
        }

        private async Task DispatchEvents(IReadOnlyList<INotification> events)
        {
            using (_connection = _sqlRepository.CreateConnection())
            {
                _connection.Open();

                using (_transaction = _connection.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        foreach (var @event in events)
                        {
                            switch (@event)
                            {
                                case ServiceMethodAddedEvent serviceMethodAdded:
                                    await AddServiceMethod(serviceMethodAdded).ConfigureAwait(false);
                                    break;
                            }
                        }

                        _transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        _transaction.Rollback();

                        _logger.Error(e);

                        throw;
                    }
                }
            }
        }

        private async Task AddServiceMethod(ServiceMethodAddedEvent @event)
        {
            await _sqlRepository.ExecuteAsync(AddServiceMethodProcedure, new
            {
                Id = @event.Id,
                Name = @event.Name
            }).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Guid>> GetServiceMethodIds()
        {
            var result = await _sqlRepository.QueryAsync<Guid>(GetServiceMethodIdsProcedure, new
            {
            }).ConfigureAwait(false);

            return result ?? new List<Guid>();
        }
    }
}
