using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PN.Logic
{
    public class Task
    {
        private int TaskId { get; set; }    
        private string Name { get; set; }
        private string Description { get; set; }
        public Task(int taskId, string name, string description)
        {
            TaskId = taskId;
            Name = name;
            Description = description;
        }
    }
}
