using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PN.Logic
{
    public class Job
    {
        public int Id { get; set; } // Id created by the DB (Primary Key)
        public int JobId { get;  set; } // Admin created ID, can be the same for multiple jobs assigned to seperate trucks
        public string Name { get;  set; } // Name of the job
        public string Description { get; set; } // Description of the job
        public int TruckId { get; set; } // Id of the truck a specific job is assigned to
    }
}
