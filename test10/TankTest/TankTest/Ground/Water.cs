using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TankTest.GridMap
{
    class Water:MapBoxPoint
    {
        public Water(Point p):base(p)
        {
            GridType =  Constant.GRIDTYPE_WATER;
            base.setAccicibility(false);
       
        } 
    }
}
