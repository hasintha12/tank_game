using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TankTest
{
    class Player
    {   
        #region "Variables"
        private string name = "";               
        private Point startP;
        private Point currentP;
        private Point prevP;
        private int direction = 0;
        private Boolean shot = false;
        private int pointsEarned = 0;
        private int coins = 0;
        private int health = Constant.PLAYER_HEALTH;   
        private bool isAlive = true;
        private bool invalidCell = false;
        private DateTime updatedTime;
        private int index = -1;
        private bool coinDropped=false;
        

        public int Coins
        {
            get { return coins; }
            set { coins = value; }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }


        public Boolean Shot
        {
            get {
                Boolean tmp = shot;
                shot = false;
                return tmp;             
            }
            set { shot = value; }
        }

        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }
        

        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        
        #endregion

        public Player(string cName,int x,int y,int dir)
        {
            name = cName;
            Point pl = new Point(x, y);
            startP=pl;
            currentP = startP;
            prevP = currentP;
            direction=dir;
        }

        #region "Properties"
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        
        public Point StartP
        {
            get { return startP; }
            set { startP = value; }
        }

        public void setCurrentP(Point p)
        {
            prevP = new Point(currentP.X,currentP.Y);
            currentP = p;
        }
        public Point getCurrentP()
        {
            return currentP;
        }

        public DateTime UpdatedTime
        {
            get { return updatedTime; }
            set { updatedTime = value; }
        }

        public int PointsEarned
        {
            get { return pointsEarned; }
            set { pointsEarned = value; }
        }

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }

        public bool InvalidCell
        {
            get { return invalidCell; }
            set { invalidCell = value; }
        }
        public Point PrevP
        {
            get { return prevP; }
        }

        public Player myclone()
        {
            Player p = new Player(this.Name, this.currentP.X, this.currentP.Y, this.Direction);
            return p;
        }
        public void setCoinDropped()
        {
            coinDropped = true;
        }
        public bool isCoinDropped()
        {
            return coinDropped;
        }
        #endregion
    }
}
