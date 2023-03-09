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

        public Task<int> AddAsync(Job entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
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
