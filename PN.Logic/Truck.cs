using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PN.Logic
{
    public class Truck
    {
        public int TruckId {  get;  set; } // ID created by the DB (Primary Key)
        public string Name { get;  set;}   // Name of the Truck
        public override string ToString() // Used to display trucks in Combo Boxes
        {
            return $"{TruckId}  {Name}";
        }

    }
}
