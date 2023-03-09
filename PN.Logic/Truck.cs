using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PN.Logic
{
    public class Truck
    {
        private int TruckId {  get; set; }
        private string Name { get; set;}
        public Truck(int truckId, string name)
        {
            TruckId = truckId;
            Name = name;
        }
        public int GetId()
        {
            return TruckId;
        }
        public override string ToString()
        {
            return $"{TruckId}  {Name}";
        }

    }
}
