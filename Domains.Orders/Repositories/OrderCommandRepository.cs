using Domains.Orders.DomainModels;
using Domains.Orders.Events;
using Domains.Orders.Interfaces;
using Infrastructure.Sql;
using Infrastructure.Sql.Interfaces;
using Logging.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Domains.Orders.Repositories
{
    public class OrderCommandRepository : IOrderCommandRepository
    {
        private const string AddOrdersProcedure = "spAddOrders";

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
            var _orderAddedEventList = new List<OrderAddedEvent>();

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
                                    _orderAddedEventList.Add(orderAdded);
                                    break;
                            }
                        }

                        await DispatchEventCollections(_orderAddedEventList).ConfigureAwait(false);

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

        private async Task DispatchEventCollections(IEnumerable<OrderAddedEvent> orderAddedEventList)
        {
            if (orderAddedEventList.Any())
                await AddOrders(orderAddedEventList).ConfigureAwait(false);
        }

        private async Task AddOrders(IEnumerable<OrderAddedEvent> @events)
        {
            await _sqlRepository.ExecuteAsync(AddOrdersProcedure, new
            {
                Orders = @events.CreateDataTable("OrderType")
            }).ConfigureAwait(false);
        }
    }
}
