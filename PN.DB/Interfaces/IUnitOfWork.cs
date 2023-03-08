using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PN.Logic;

namespace PN.DB.Interfaces
{
    public interface IUnitOfWork
    {
        ITruckRepository<Truck> Trucks { get; }
        IJobRepository<Job> Jobs { get; }
        IEmployeeRepository<Employee> Employees { get; }
    }
}
