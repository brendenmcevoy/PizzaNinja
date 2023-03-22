using Dapper;
using PN.DB.Interfaces;
using PN.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PN.DB.Repository
{
    public class TruckRepository<Truck> : ITruckRepository<Truck>
    {
        private readonly IConnectionFactory _connectionFactory;
        public TruckRepository(IConnectionFactory Conn)
        {
            _connectionFactory = Conn;
        }

        public async Task<int> AddAsync(Truck entity)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "INSERT into Truck (Name) VALUES (@Name)";
                connection.Open();
                var result = await Task.Run(() => connection.ExecuteAsync(sql, entity));
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "DELETE FROM Truck WHERE TruckId = @Id";
                connection.Open();
                var result = await Task.Run(() => connection.ExecuteAsync(sql, new { Id = id }));
                return result;
            }
        }

        public async Task<List<Truck>> GetAllAsync()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "SELECT * FROM Truck";
                connection.Open();
                var result = await Task.Run(() => SqlMapper.Query<Truck>(connection,sql).ToList());
                return result;
            }
        }

        public async Task<Truck> GetByIdAsync(int id)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "SELECT * FROM Jobs WHERE TruckId = @Truckid";
                connection.Open();
                var result = await Task.Run(() => connection.QueryFirstOrDefault<Truck>(sql, new { TruckId = id }));
                return result;
            }
        }

        public async Task<int> UpdateAsync(Truck entity)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "UPDATE Truck SET Name = @Name WHERE TruckId = @TruckId";
                connection.Open();
                var result = await Task.Run(() => connection.Execute(sql, entity));
                return result;
            }
        }
    }
}
