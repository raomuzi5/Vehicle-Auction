using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
namespace Proji.Persistence
{
    public class DataAccess
    {
        private readonly string _connectionString;

        public DataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection Connection => new SqlConnection(_connectionString);

        public async Task<T> QuerySingleAsync<T>(string storedProcedure, object param = null)
        {
            using (var dbConnection = Connection)
            {
                return await dbConnection.QuerySingleOrDefaultAsync<T>(storedProcedure, param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string storedProcedure, object param = null)
        {
            using (var dbConnection = Connection)
            {
                return await dbConnection.QueryAsync<T>(storedProcedure, param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> ExecuteAsync(string storedProcedure, object param = null)
        {
            using (var dbConnection = Connection)
            {
                return await dbConnection.ExecuteAsync(storedProcedure, param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
