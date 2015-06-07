using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TankTest
{   /*defining grid locations*/
    class MapBoxPoint
    {
        private Point mapPoint;
        private Boolean accecible = false;        
        private String gridType; 

        
        public MapBoxPoint(Point p)
        {
            mapPoint = p;
            gridType = Constant.GRIDTYPE_NORMAL;
        }
        public Point giveMapPoint()
        {
            return mapPoint;
        }
        public String giveMapPointString()
        {
            return ((int)mapPoint.X * 10 + (int)mapPoint.Y+"");
        }
        public void setAccicibility(Boolean accicibility)
        {
            accecible = accicibility;
        }
        public Boolean isAccicible()
        {
            return accecible;
        }
        
        public String GridType
        {
            set{gridType=value;}
            get{return gridType;}
        }
       

    }

}
