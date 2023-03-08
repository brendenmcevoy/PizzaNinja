using PN.DB.Interfaces;
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
        Task<int> IGenericRepository<Job>.AddAsync(Job entity)
        {
            throw new NotImplementedException();
        }

        Task<int> IGenericRepository<Job>.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<List<Job>> IGenericRepository<Job>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<Job> IGenericRepository<Job>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<int> IGenericRepository<Job>.UpdateAsync(Job entity)
        {
            throw new NotImplementedException();
        }
    }
}
