using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TankTest.GridMap
{
    /*stone barriors always acces denied*/
    class Stone:MapBoxPoint
    {
        public Stone(Point p): base(p)
        {

            GridType = Constant.GRIDTYPE_STONE;
            base.setAccicibility(false);
        }
    }
}
