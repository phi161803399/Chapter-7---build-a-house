using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter_7___build_a_house
{
    class Outside: Location
    {
        private bool hot;

        public Outside(string name, bool hot):base(name)
        {
            this.hot = hot;
        }
        public override string Description
        {
            get
            {
                string description = base.Description;
                if (this.hot == true)
                {
                    description += "It's very hot here.";
                }

                return description;
            }
        }
    }
}
