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
        Task<string> GetUsernameAsync(string username);
        Task<Employee> GetEmployeeByUsernameAsync(string username);
        Task<string> GetPasswordAsync(string password);
    }
}
