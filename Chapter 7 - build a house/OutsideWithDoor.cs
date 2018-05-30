using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_7___build_a_house
{
    class OutsideWithDoor : Outside, IHasExteriorDoor
    {
        public string DoorDescription { get; }

        public Location DoorLocation { get; set; }

        public OutsideWithDoor(string name, bool hot, string doordescription) : base(name, hot)
        {
            DoorDescription = doordescription;
        }
    }
}
