using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_7___build_a_house
{
    abstract class Location
    {
        public Location(string name)
        {
            Name = name;
        }
        public Location[] Exits;
        public string Name { get; private set; }

        public virtual string Description
        {
            get
            {
                string description = $"You're standing in the {Name}." +
                                     $"You see exits to the following places: ";
                for (int i = 0;i<Exits.Length;i++)
                {
                    description += $" {Exits[i]}";
                    if (i != Exits.Length - 1)
                        description += ",";
                }

                description += ".";
                return description;
            }
        }
    }
}
