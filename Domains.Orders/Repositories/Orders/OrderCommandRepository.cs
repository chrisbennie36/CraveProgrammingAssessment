using Domains.Ordering.DomainModels.Orders;
using Domains.Ordering.Events.Orders;
using Domains.Ordering.Interfaces.Orders;
using Infrastructure.Sql.Interfaces;
using Logging.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Domains.Ordering.Repositories.Orders
{
    public class OrderCommandRepository : IOrderCommandRepository
    {
        private const string AddOrdersProcedure = "spAddOrder";

        private IDbConnection _connection = null;
        private IDbTransaction _transaction = null;

        private readonly ISqlRepository _sqlRepository;
        private readonly ILogger _logger;

        public OrderCommandRepository(ISqlRepository sqlRepository, ILogger logger)
        {
            _sqlRepository = sqlRepository ?? throw new ArgumentNullException(nameof(sqlRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Store(Order order)
        {
            if (!order.HasChanges())
            {
                return;
            }

            await DispatchEvents(order.PendingChanges).ConfigureAwait(false);
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
                                case OrderAddedEvent orderAdded:
                                    await AddOrder(orderAdded).ConfigureAwait(false);
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

        private async Task AddOrder(OrderAddedEvent @event)
        {
            await _sqlRepository.ExecuteAsync(AddOrdersProcedure, new
            {
                Id = @event.Id,
                ProductId = @event.ProductId,
                ServiceMethodId = @event.ServiceMethodId,
                Additions = @event.Additions,
                PreparationMethod = @event.PreparationMethod,
                CustomerName = @event.CustomerName,
                CustomerPhoneNumber = @event.CustomerPhoneNumber,
                CustomerEmail = @event.CustomerEmail
            }).ConfigureAwait(false);
        }
    }
}
