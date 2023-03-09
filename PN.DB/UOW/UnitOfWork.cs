using PN.DB.Interfaces;
using PN.DB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PN.Logic;


namespace PN.DB.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IConnectionFactory Conn) 
        {
            Jobs = new JobRepository<Job>(Conn);
            Employees = new EmployeeRepository<Employee>(Conn);
            Trucks = new TruckRepository<Truck>(Conn);
            CompletedJobs = new CompletedJobRepository<CompletedJob>(Conn);
        }

        public IJobRepository<Job> Jobs { get; private set; }
        public IEmployeeRepository<Employee> Employees { get; private set; }
        public ITruckRepository<Truck> Trucks { get; private set; }
        public ICompledJobRepository<CompletedJob> CompletedJobs { get; private set; }
    }
}
