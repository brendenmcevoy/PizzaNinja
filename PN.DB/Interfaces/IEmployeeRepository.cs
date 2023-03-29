using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PN.Logic;

namespace PN.DB.Interfaces
{
    public interface IEmployeeRepository<Employee> : IGenericRepository<Employee>
    {
        Task<string> GetUsernameAsync(string username); // Get employee username from DB
        Task<Employee> GetEmployeeByUsernameAsync(string username); // Get employee with matching username from DB
        Task<string> GetPasswordAsync(int id); // Get employee password
    }
}
