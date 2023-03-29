using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PN.DB.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<T> GetByIdAsync(int id); // Get object using ID
        Task<List<T>> GetAllAsync(); // Gets all objects from DB
        Task<int> AddAsync(T entity); // Add an object to DB
        Task<int> UpdateAsync(T entity); // Update an object in DB
        Task<int> DeleteAsync(int id); // Delete object from DB
    }
}
