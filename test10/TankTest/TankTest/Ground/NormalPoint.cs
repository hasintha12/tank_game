using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TankTest.GridMap
{
    class NormalPoint:MapBoxPoint
    {
        private Boolean occupied =false;

        public NormalPoint(System.Drawing.Point p)
            : base(p)
        {
            base.GridType = Constant.GRIDTYPE_NORMAL;
            base.setAccicibility(true);
        }
        public void setOccupied()
        {
            if (isAccicible())
            {
                occupied = true;
            }
        }
        public void setUnoccupied()
        {
            if (isOccupied())
            {
                occupied = false;
            }
        }
        public Boolean isOccupied()
        {
            return occupied;
        }
    }
}
