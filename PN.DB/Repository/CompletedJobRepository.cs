using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using PN.DB.Interfaces;
using PN.Logic;

namespace PN.DB.Repository
{
    public class CompletedJobRepository<CompletedJob> : ICompledJobRepository<CompletedJob>
    {
        private readonly IConnectionFactory _connectionFactory;
        public CompletedJobRepository(IConnectionFactory Conn)
        {
            _connectionFactory = Conn;
        }
        public async Task<int> AddAsync(CompletedJob entity)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "INSERT into CompletedJobs (JobId,EmployeeId,TruckId,Date,Notes,Name,Description) VALUES (@JobId,@EmployeeId,@TruckId,@Date,@Notes,@Name,@Description)";
                connection.Open();
                var result = await Task.Run(() => connection.ExecuteAsync(sql, entity));
                return result;
            }
        }
        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CompletedJob>> GetAllAsync()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "SELECT * FROM CompletedJobs";
                connection.Open();
                var result = await Task.Run(() => SqlMapper.Query<CompletedJob>(connection, sql).ToList());
                return result;
            }
        }

        public Task<CompletedJob> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(CompletedJob entity)
        {
            throw new NotImplementedException();
        }
    }
}
