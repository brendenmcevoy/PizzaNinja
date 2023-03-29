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

        public async Task<int> AddAsync(Employee entity) // Adds an Employee to the DB
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "INSERT into Employee (Name,IsAdmin,Username,Password) VALUES (@Name,@IsAdmin,@Username,@Password)";
                connection.Open();
                var result = await Task.Run(() => connection.ExecuteAsync(sql,entity));
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id) // Removes an Employee from the DB
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "DELETE FROM Employee WHERE Id = @Id";
                connection.Open();
                var result = await Task.Run(() => connection.ExecuteAsync(sql, new {Id = id}));
                return result;
            }
        }

        public async Task<List<Employee>> GetAllAsync() // Gets all Employees from the DB
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "SELECT * FROM Employee";
                connection.Open();
                var result = await Task.Run(() => SqlMapper.Query<Employee>(connection, sql).ToList());
                return result;
            }
        }

        public async Task<Employee> GetByIdAsync(int id) // Gets an Employee with matching Id from the DB
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "SELECT * FROM Employee WHERE Id = @id";
                connection.Open();
                var result = await Task.Run(() => connection.QueryFirstOrDefault<Employee>(sql, new { Id = id}));
                return result;
            }
        }

        public async Task<string> GetPasswordAsync(int id) // Gets a Employees password from DB
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "SELECT Password FROM Employee WHERE (Id = @id)";
                connection.Open();
                var result = await Task.Run(() => connection.QueryFirstOrDefaultAsync<string>(sql, new { Id = id }));
                return result;
            }
        }

        public async Task<string> GetUsernameAsync(string username) // Gets an Employees username from the DB
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "SELECT Username FROM Employee WHERE (Username = @username)";
                connection.Open();
                var result = await Task.Run(() => connection.QueryFirstOrDefaultAsync<string>(sql, new {Username = username}).Result);
                return result;
            }
        }

        public async Task<int> UpdateAsync(Employee entity) // Updates a Emplyoee in the DB
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "UPDATE Employee SET Name = @Name, IsAdmin = @IsAdmin, Username = @Username, Password = @Password WHERE Id = @Id";
                connection.Open();
                var result = await Task.Run(() => connection.Execute(sql, entity));
                return result;
            }
        }

        public async Task<Employee> GetEmployeeByUsernameAsync(string username) // Gets an employee with a matching username from DB
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
