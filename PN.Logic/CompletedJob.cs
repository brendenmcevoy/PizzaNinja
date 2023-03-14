using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PN.Logic
{
    public class CompletedJob
    {
        public int Id { get; private set; }
        public int JobId { get; private set; }
        public int EmployeeId { get; private set; }
        public int TruckId { get; private set; }
        public string Date { get; private set; }
        public string Notes { get; private set; }  //Can be NULL

        public CompletedJob(int id, int jobId, int employeeId, int truckId, string date, string notes)
        {
            Id = id;
            JobId=jobId;
            EmployeeId=employeeId;
            TruckId=truckId;
            Date = date;
            Notes = notes;
        }
        public CompletedJob(int id, int jobId, int employeeId, int truckId, string date) 
        {
            Id = id;
            JobId=jobId;
            EmployeeId=employeeId;
            TruckId=truckId;
            Date = date;
            Notes = "No notes entered.";
        }
    }
}
