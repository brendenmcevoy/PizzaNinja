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
        public string Description { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Notes { get;  set; }  //Can be NULL

    }
}
