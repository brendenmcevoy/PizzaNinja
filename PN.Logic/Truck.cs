using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PN.Logic
{
    public class Truck
    {
        public int TruckId {  get;  set; }
        public string Name { get;  set;}   
        public override string ToString()
        {
            return $"{TruckId}  {Name}";
        }

    }
}
