using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TankTest.GridMap
{
    class Brick:NormalPoint
    {
        private int damageLevel;
        public Brick(System.Drawing.Point p)
            : base(p)
        {
            damageLevel = 0;//if no damage to brick wall
            GridType = Constant.GRIDTYPE_BRICK;
            base.setAccicibility(false);
        }
        public void setDamageLevel(int dLevel)
        {
            damageLevel = dLevel;
            if (damageLevel == 4)//if brickwall damage =100%
            {
                setAccicibility(true);
            }
        }
        public int getDamageLevel()
        {
            return damageLevel;
        }
    }
}
