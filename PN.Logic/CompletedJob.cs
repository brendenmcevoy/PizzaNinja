using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PN.Logic
{
    public class CompletedJob
    {
        public int Id { get; set; } // ID that is created by the DB (Primary Key)
        public int JobId { get; set; } // ID that is created by an Admin (Inherited from Job)
        public int EmployeeId { get; set; } // Id of the Employee that completed the job
        public int TruckId { get; set; } // Id of the truck that the job was assigned to
        public string Description { get; set; } // Description of the job
        public string Name { get; set; } // Name of the job
        public string Date { get; set; } // Date that the job was completed
        public string Notes { get;  set; }  // Notes added by the Employee that completed the job

    }
}
