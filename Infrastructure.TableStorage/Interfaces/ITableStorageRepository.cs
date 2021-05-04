using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.TableStorage.Interfaces
{
    public interface ITableStorageRepository
    {
        CloudTableClient GetConnection();
        Task ExecuteAsync(string tableReference, ITableEntity entity);
        Task<IEnumerable<T>> QueryAll<T>(string tableReference, T entity) where T : ITableEntity, new();
        Task<IEnumerable<T>> Query<T>(string tableReference, TableQuery<T> query) where T : ITableEntity, new();
        T QuerybyId<T>(string tableReference, Guid id) where T : ITableEntity, new();
    }
}
