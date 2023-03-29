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
        public async Task<int> AddAsync(CompletedJob entity) // Adds a Completed Job to the DB
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "INSERT into CompletedJobs (JobId,EmployeeId,TruckId,Date,Notes,Name,Description) VALUES (@JobId,@EmployeeId,@TruckId,@Date,@Notes,@Name,@Description)";
                connection.Open();
                var result = await Task.Run(() => connection.ExecuteAsync(sql, entity));
                return result;
            }
        }
        public async Task<int> DeleteAsync(int id) // Deletes a Completed Job from the DB
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "Delete FROM CompletedJobs";
                connection.Open();
                var result = await Task.Run(() => SqlMapper.ExecuteAsync(connection, sql));
                return result;
            }
        }

        public async Task<List<CompletedJob>> GetAllAsync() // Get all Completed Jobs from the DB
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "SELECT * FROM CompletedJobs";
                connection.Open();
                var result = await Task.Run(() => SqlMapper.Query<CompletedJob>(connection, sql).ToList());
                return result;
            }
        }

        public async Task<CompletedJob> GetByIdAsync(int id) // Get specific Completed Job with matching id from the DB (NOT CURRENTLY USED)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "SELECT * FROM CompletedJobs WHERE Id = @id";
                connection.Open();
                var result = await Task.Run(() => connection.QueryFirstOrDefaultAsync(sql, new { Id = id }));
                return result;
            }
        }

        public async Task<int> UpdateAsync(CompletedJob entity) // Updates a Completed Job in the DB (NOT CURRENTLY USED)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "UPDATE Employee SET Name = @Name, JobId = @JobId, EmployeeId = @EmployeeId, TruckId = @TruckId, Date = @Date, Notes = @Notes, Description = @Description WHERE JobId = @JobId";
                connection.Open();
                var result = await Task.Run(() => connection.Execute(sql, entity));
                return result;
            }
        }
    }
}
