using PN.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using PN.Logic;

namespace PN.DB.Repository
{
    public class EmployeeRepository<Employee> : IEmployeeRepository<Employee>
    {
        private readonly IConnectionFactory _connectionFactory;
        public EmployeeRepository(IConnectionFactory Conn)
        {
             _connectionFactory = Conn;
        }

        public async Task<int> AddAsync(Employee entity)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "INSERT into Employee (Name,IsAdmin,Username,Password) VALUES (@Name,@IsAdmin,@Username,@Password)";
                connection.Open();
                var result = await Task.Run(() => connection.ExecuteAsync(sql,entity));
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "DELETE FROM Employee WHERE Id = @Id";
                connection.Open();
                var result = await Task.Run(() => connection.ExecuteAsync(sql, new {Id = id}));
                return result;
            }
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "SELECT * FROM Employee";
                connection.Open();
                var result = await Task.Run(() => SqlMapper.Query<Employee>(connection, sql).ToList());
                return result;
            }
        }

        public Task<Employee> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetPasswordAsync(string password)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "SELECT Password FROM Employee WHERE (Password = @password)";
                connection.Open();
                var result = await Task.Run(() => connection.QueryFirstOrDefaultAsync<string>(sql, new { Password = password }).Result);
                return result;
            }
        }

        public async Task<string> GetUsernameAsync(string username)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "SELECT Username FROM Employee WHERE (Username = @username)";
                connection.Open();
                var result = await Task.Run(() => connection.QueryFirstOrDefaultAsync<string>(sql, new {Username = username}).Result);
                return result;
            }
        }

        public Task<int> UpdateAsync(Employee entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> GetEmployeeByUsernameAsync(string username)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "SELECT * FROM Employee WHERE (Username = @username)";
                connection.Open();
                var result = await Task.Run(() => connection.QueryFirstOrDefaultAsync<Employee>(sql, new { Username = username }).Result);
                return result;
            }
        }
    }
}
