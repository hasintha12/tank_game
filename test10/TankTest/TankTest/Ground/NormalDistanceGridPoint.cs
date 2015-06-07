using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TankTest.GridMap
{
    class NormalDistanceGridPoint:NormalPoint
    {
        private int distance;
        private int dir;
        private NormalDistanceGridPoint prev;

        public NormalDistanceGridPoint(System.Drawing.Point p)
            :base(p)
        {

        }

        public int Distance
        {
            set { distance = value; }
            get { return distance;}
        }
        public int Dir
        {
            set { dir = value; }
            get { return dir; }
        }
        public NormalDistanceGridPoint Prev
        {
            set { prev = value; }
            get { return prev; }
        }

        internal NormalDistanceGridPoint Clone()
        {
            NormalDistanceGridPoint p = new NormalDistanceGridPoint(this.giveMapPoint());
            p.Distance = this.Distance;
            p.Dir = this.Dir;
            p.Prev = this.Prev;
            p.setAccicibility(this.isAccicible());            
            return p;
        }
    }
}
