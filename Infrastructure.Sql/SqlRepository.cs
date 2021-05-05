using Dapper;
using Infrastructure.RepositoryConfiguration;
using Infrastructure.Sql.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Infrastructure.Sql
{
    public class SqlRepository : ISqlRepository
    {
        private readonly IOptions<ConnectionStringsConfiguration> _connectionStringsConfiguration;

        public SqlRepository(IOptions<ConnectionStringsConfiguration> connectionSStringsConfiguration)
        {
            _connectionStringsConfiguration = connectionSStringsConfiguration ?? throw new ArgumentNullException(nameof(connectionSStringsConfiguration));
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionStringsConfiguration.Value.SqlConnectionString);
        }

        public async Task ExecuteAsync(string command, object args)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    await connection.ExecuteAsync(command, args, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<TResult>> QueryAsync<TResult>(string command, object args)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    return await connection.QueryAsync<TResult>(command, args, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
