using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PN.Logic
{
    public class CompletedJob
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int EmployeeId { get; set; }
        public int TruckId { get; set; }
        private string Date { get; set; }
        private string Notes { get; set; }  //Can be NULL

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
