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
        private string Name { get; set; }
        private string Description { get; set; }
        public Job(int jobId, string name, string description)
        {
            JobId = jobId;
            Name = name;
            Description = description;
        }
    }
}
