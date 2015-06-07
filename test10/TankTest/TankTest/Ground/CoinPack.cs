using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TankTest.GridMap
{
    class Coin:NormalPoint
    {
        private int value, time;
        public Coin(System.Drawing.Point p, int val, int time):base(p)
        {
            this.value = val;
            this.time = time;
            GridType = Constant.GRIDTYPE_COIN;
            base.setAccicibility(true);
        }
        public int Value
        {
            set { this.value = value; }
            get { return this.value; }
        }
        public int Time
        {
            set { this.time = value; }
            get { return this.time; }
        }
        
        public void reduceTime()//countdown for dissapearing coin pack
        {
            time -= Constant.COINLIFE_REFRESHDELAY;
        }
    }
}
