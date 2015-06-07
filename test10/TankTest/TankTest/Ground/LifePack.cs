using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TankTest.GridMap
{
    class LifePack:NormalPoint
    {
        private int time;
        public LifePack(System.Drawing.Point p, int time)
            : base(p)
        {
            this.time = time;
            GridType = Constant.GRIDTYPE_LIFEPACK;
            base.setAccicibility(true);
        }
        public int Time
        {
            set { time = value; }
            get { return time; }
        }
        public void reduceTime()//countdown for diaaspearing cion pack
        {
            time -= Constant.COINLIFE_REFRESHDELAY;
        }
    }
}
