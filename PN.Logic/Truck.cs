using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PN.Logic
{
    public class Truck
    {
        public int TruckId {  get; private set; }
        public string Name { get; private set;} 
        public Truck(int truckId, string name)
        {
            TruckId = truckId;
            Name = name;
        }
        public Truck(int truckId) { TruckId = truckId; Name = "Unnamed"; }      
        public override string ToString()
        {
            return $"{TruckId}  {Name}";
        }

    }
}
