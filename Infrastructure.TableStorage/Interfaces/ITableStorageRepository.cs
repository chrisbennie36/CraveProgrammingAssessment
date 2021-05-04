using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.TableStorage.Interfaces
{
    public interface ITableStorageRepository
    {
        CloudTableClient GetConnection();
        Task InsertAsync(string tableReference, ITableEntity entity);
        Task UpdateAsync(string tableReference, ITableEntity entity);
        Task<IEnumerable<T>> QueryAllAsync<T>(string tableReference) where T : ITableEntity, new();
        Task<IEnumerable<T>> QueryAsync<T>(string tableReference, TableQuery<T> query) where T : ITableEntity, new();
        T QuerybyIdAsync<T>(string tableReference, Guid id) where T : ITableEntity, new();
    }
}
