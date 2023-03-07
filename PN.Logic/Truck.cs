using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PN.Logic
{
    public class Truck
    {
        private int TruckId { get; set; }
        private string TruckName { get; set;}
        public Truck(int truckId, string truckName)
        {
            TruckId = truckId;
            TruckName = truckName;
        }
    }
}
