using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.RepositoryConfiguration;
using Infrastructure.TableStorage.Interfaces;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Options;

namespace Infrastructure.TableStorage
{
    public class TableStorageRepository : ITableStorageRepository
    {
        private readonly IOptions<ConnectionStringsConfiguration> _connectionStringsConfiguration;

        public TableStorageRepository(IOptions<ConnectionStringsConfiguration> connectionStringsConfiguration)
        {
            _connectionStringsConfiguration = connectionStringsConfiguration ?? throw new ArgumentNullException(nameof(connectionStringsConfiguration));
        }

        public CloudTableClient GetConnection()
        {
            var account = CloudStorageAccount.Parse(_connectionStringsConfiguration.Value.TableStorageConnectionString);
            return account.CreateCloudTableClient();
        }

        public async Task InsertAsync(string tableReference, ITableEntity entity)
        {
            var client = GetConnection();

            var table = client.GetTableReference(tableReference);
            table.CreateIfNotExists();

            var tableOperation = TableOperation.Insert(entity);

            await table.ExecuteAsync(tableOperation).ConfigureAwait(false);
        }

        public async Task UpdateAsync(string tableReference, ITableEntity entity)
        {
            var client = GetConnection();

            var table = client.GetTableReference(tableReference);
            table.CreateIfNotExists();

            var tableOperation = TableOperation.Replace(entity);

            await table.ExecuteAsync(tableOperation).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> QueryAllAsync<T>(string tableReference) where T : ITableEntity, new()
        {
            var client = GetConnection();

            var table = client.GetTableReference(tableReference);

            TableContinuationToken token = null;
            var entities = new List<T>();
            do
            {
                var queryResult = await table.ExecuteQuerySegmentedAsync(new TableQuery<T>(), token).ConfigureAwait(false);
                entities.AddRange(queryResult.Results);
                token = queryResult.ContinuationToken;
            } while (token != null);

            return entities;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string tableReference, TableQuery<T> query) where T : ITableEntity, new()
        {
            var client = GetConnection();

            var table = client.GetTableReference(tableReference);
            TableContinuationToken token = null;
            var entities = new List<T>();
            do
            {
                var queryResult = await table.ExecuteQuerySegmentedAsync(query, token).ConfigureAwait(false);
                entities.AddRange(queryResult.Results);
                token = queryResult.ContinuationToken;
            } while (token != null);

            return entities;
        }

        public async Task<T> QuerybyIdAsync<T>(string tableReference, Guid id) where T : ITableEntity, new()
        {
            var client = GetConnection();
            var table = client.GetTableReference(tableReference);

            var condition = TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id.ToString());
            var query = new TableQuery<T>().Where(condition);

            var result = await Task.Run(() => table.ExecuteQuery(query)).ConfigureAwait(false);

            return result.FirstOrDefault();
        }
    }
}
