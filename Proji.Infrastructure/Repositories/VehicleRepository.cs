using Proji.Domain.Entities;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Proji.Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly string _connectionString;

        public VehicleRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection Connection => new SqlConnection(_connectionString);

        public async Task<Vehicle> GetVehicleByIdAsync(int id)
        {
            using (var dbConnection = Connection)
            {
                var parameters = new { Id = id };
                return await dbConnection.QuerySingleOrDefaultAsync<Vehicle>("GetVehicleById", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
        {
            using (var dbConnection = Connection)
            {
                return await dbConnection.QueryAsync<Vehicle>("GetAllVehicles", commandType: CommandType.StoredProcedure);
            }
        }

        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            using (var dbConnection = Connection)
            {
                var parameters = new
                {
                    BasePrice = vehicle.BasePrice,
                    CarType = vehicle.CarType.ToString()  // Assuming CarType is an enum and you want to pass its string representation
                };

                await dbConnection.ExecuteAsync("AddVehicle", parameters, commandType: CommandType.StoredProcedure);
            }
        }


        public async Task UpdateVehicleAsync(Vehicle vehicle)
        {
            using (var dbConnection = Connection)
            {
                var parameters = new
                {
                    Id = vehicle.Id,
                    BasePrice = vehicle.BasePrice,
                    CarType = vehicle.CarType.ToString()  // Assuming CarType is an enum and you want to pass its string representation
                };

                await dbConnection.ExecuteAsync("UpdateVehicle", parameters, commandType: CommandType.StoredProcedure);
            }
        }


        public async Task DeleteVehicleAsync(int id)
        {
            using (var dbConnection = Connection)
            {
                var parameters = new { Id = id };
                await dbConnection.ExecuteAsync("DeleteVehicle", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
