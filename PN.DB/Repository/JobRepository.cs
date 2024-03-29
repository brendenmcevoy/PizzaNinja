﻿using Dapper;
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

        public async Task<int> AddAsync(Job entity) // Adds a job to the DB
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "INSERT into Jobs (JobId,Name,Description,TruckId) VALUES (@JobId,@Name,@Description,@TruckId)";
                connection.Open();
                var result = await Task.Run(() => connection.ExecuteAsync(sql, entity));
                return result;
            }
        }
        public async Task<int> DeleteByIdAsync(int id, int truckId) // Deletes a job with matching jobid and truck id from the DB
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "DELETE FROM Jobs WHERE JobId = @JobId AND TruckId = @TruckId";
                connection.Open();
                var result = await Task.Run(() => connection.ExecuteAsync(sql, new { JobId = id, TruckId = truckId }));
                return result;
            }
        }
        public async Task<int> DeleteAsync(int id) // Deletes a job from the DB
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "DELETE FROM Jobs WHERE Id = @Id";
                connection.Open();
                var result = await Task.Run(() => connection.ExecuteAsync(sql, new { Id = id}));
                return result;
            }
        }
        public async Task<List<Job>> GetAllAsync() // Gets all jobs from the DB
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "SELECT * FROM Jobs";
                connection.Open();
                var result = await Task.Run(() => SqlMapper.Query<Job>(connection, sql).ToList());
                return result;
            }
        }
        public async Task<List<Job>> GetAllByIdAsync(int id) // Gets all the jobs with a matching TruckId from the DB
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "SELECT * FROM Jobs WHERE TruckId = @TruckId";
                connection.Open();
                var result = await Task.Run(() => SqlMapper.Query<Job>(connection, sql, new { TruckId = id}).ToList());
                return result;
            }
        }

        public async Task<Job> GetByIdAsync(int id) // Gets a job with matching id from a DB
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "SELECT * FROM Jobs WHERE Id = @id";
                connection.Open();
                var result = await Task.Run(() => connection.QueryFirstOrDefault<Job>(sql, new { Id = id }));
                return result;
            }
        }

        public async Task<int> UpdateAsync(Job entity) // Updates a job in the DB
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var sql = "UPDATE Jobs SET JobId = @JobId, Name = @Name, Description = @Description, TruckId = @TruckId WHERE Id = @Id";
                connection.Open();
                var result = await Task.Run(() => connection.Execute(sql, entity));
                return result;
            }
        }
    }
}
