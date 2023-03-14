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
    public class JobRepository<Job> : IJobRepository<Job>
    {
        private readonly IConnectionFactory _connectionFactory;
        public JobRepository(IConnectionFactory Conn)
        {
            _connectionFactory = Conn;
        }

        public async Task<int> AddAsync(Job entity)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "INSERT into Jobs (JobId,Name,Description,TruckId) VALUES (@JobId,@Name,@Description,@TruckId)";
                connection.Open();
                var result = await Task.Run(() => connection.ExecuteAsync(sql, entity));
                return result;
            }
        }
        public async Task<int> DeleteByIdAsync(int id,int truckId)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "DELETE FROM Jobs WHERE JobId = @JobId AND TruckId = @TruckId";
                connection.Open();
                var result = await Task.Run(() => connection.ExecuteAsync(sql, new { JobId = id, TruckId = truckId }));
                return result;
            }
        }
        public async Task<int> DeleteAsync(int id)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "DELETE FROM Jobs WHERE JobId = @JobId";
                connection.Open();
                var result = await Task.Run(() => connection.ExecuteAsync(sql, new { JobId = id}));
                return result;
            }
        }

        public async Task<List<Job>> GetAllAsync()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "SELECT * FROM Jobs";
                connection.Open();
                var result = await Task.Run(() => SqlMapper.Query<Job>(connection, sql).ToList());
                return result;
            }
        }
        public async Task<List<Job>> GetAllByIdAsync(int id)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "SELECT * FROM Jobs WHERE TruckId = @TruckId";
                connection.Open();
                var result = await Task.Run(() => SqlMapper.Query<Job>(connection, sql, new { TruckId = id}).ToList());
                return result;
            }
        }

        public Task<Job> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Job entity)
        {
            throw new NotImplementedException();
        }
    }
}
