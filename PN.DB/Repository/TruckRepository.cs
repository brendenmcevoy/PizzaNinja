using Dapper;
using PN.DB.Interfaces;
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

        public Task<int> AddAsync(Truck entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
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

        public Task<Truck> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Truck entity)
        {
            throw new NotImplementedException();
        }
    }
}
