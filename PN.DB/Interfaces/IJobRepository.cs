using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PN.DB.Interfaces
{
    public interface IJobRepository<Job> : IGenericRepository<Job>
    {
        Task <List<Job>> GetAllByIdAsync(int id); // Get all jobs with matching ID from DB
        Task<int> DeleteByIdAsync(int id, int truckId); // Delete job with matching Id AND TruckId from DB
    }
}
