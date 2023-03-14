using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PN.Logic
{
    public class Job
    {
        public int JobId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int TruckId { get; private set; }
        public Job(int jobId, string name, string description, int truckId)
        {
            JobId = jobId;
            Name =  name;
            Description = description;
            TruckId = truckId;
        }
    }
}
