using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PN.Logic
{
    public class Job
    {
        private int JobId { get; set; }
        public int _jobId { get; private set; }
        private string Name { get; set; }
        public string _name { get; private set; }
        private string Description { get; set; }
        public string _description { get; private set; }
        private int TruckId { get; set; }
        public Job(int jobId, string name, string description, int truckId)
        {
            JobId = _jobId = jobId;
            Name = _name = name;
            Description = _description = description;
            TruckId = truckId;
        }
    }
}
