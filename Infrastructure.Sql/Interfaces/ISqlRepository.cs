using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Infrastructure.Sql.Interfaces
{
    public interface ISqlRepository
    {
        IDbConnection CreateConnection();
        Task ExecuteAsync(string command, object args);
        Task<IEnumerable<TResult>> QueryAsync<TResult>(string command, object args);
    }
}
