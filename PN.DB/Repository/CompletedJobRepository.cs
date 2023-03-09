﻿using System;
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
        public Task<int> AddAsync(CompletedJob entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CompleteJobAsync(CompletedJob entity)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "INSERT into CompletedJobs (Id,JobId,EmployeeId,TruckId) VALUES (@Id,@JobId,@EmployeeId,@TruckId)";
                connection.Open();
                var result = await Task.Run(() => connection.ExecuteAsync(sql, entity));
                return result;
            }
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CompletedJob>> GetAllAsync()
        {
            throw new NotImplementedException();
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
