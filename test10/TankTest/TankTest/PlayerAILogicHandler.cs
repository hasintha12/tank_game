using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TankTest
{
    class PlayerAILogicHandler
    {
        private GridHandler gHandler;
        private Player player;
        GridMap.NormalDistanceGridPoint[,] playerDistanceGrid;
        GridMap.NormalDistanceGridPoint nearestTarget;

        public PlayerAILogicHandler(GridHandler gHandler, Player p)
        {
            this.gHandler = gHandler;
            //this.player = p;
            this.player = gHandler.givePlayerHandler().givePlayer(gHandler.givePlayerHandler().giveMyPlayerIndex());
            playerDistanceGrid = new GridMap.NormalDistanceGridPoint[Constant.MAP_SIZE, Constant.MAP_SIZE];
        }

        private void calcDistances()
        {
            for (int i = 0; i < Constant.MAP_SIZE; i++)
            {
                for (int j = 0; j < Constant.MAP_SIZE; j++)
                {
                    GridMap.NormalDistanceGridPoint tmppoint = new GridMap.NormalDistanceGridPoint(new System.Drawing.Point(j, i));
                    tmppoint.Distance = int.MaxValue;
                    tmppoint.Prev = null;
                    tmppoint.Dir = 0;
                    playerDistanceGrid[j, i] = tmppoint;
                }
            }
            Queue<MapBoxPoint> q = new Queue<MapBoxPoint>();
            MapBoxPoint point = gHandler.giveGridPoint(player.getCurrentP().X, player.getCurrentP().Y);
            GridMap.NormalDistanceGridPoint normalPoint = playerDistanceGrid[player.getCurrentP().X, player.getCurrentP().Y];
            normalPoint.Distance = 0;
            normalPoint.Dir = player.Direction;
            normalPoint.Prev = null;
            q.Enqueue(point);
            while (q.Count != 0)
            {
                point = q.Dequeue();
                normalPoint = playerDistanceGrid[point.giveMapPoint().X, point.giveMapPoint().Y];
                MapBoxPoint point2;
                GridMap.NormalDistanceGridPoint normalpoint2;
                if (normalPoint.Dir == 0)
                {
                    if ((point.giveMapPoint().Y - 1) >= 0)
                    {
                        point2 = gHandler.giveGridPoint(point.giveMapPoint().X, point.giveMapPoint().Y - 1);
                        normalpoint2 = playerDistanceGrid[point.giveMapPoint().X, point.giveMapPoint().Y - 1];
                        if (point2.isAccicible() && normalpoint2.Distance > normalPoint.Distance + 1)
                        {
                            normalpoint2.Distance = normalPoint.Distance + 1;
                            normalpoint2.Dir = normalPoint.Dir;
                            normalpoint2.Prev = normalPoint;
                            q.Enqueue(point2);                            
                        }
                    }
                    if ((point.giveMapPoint().Y + 1) < Constant.MAP_SIZE)
                    {
                        point2 = gHandler.giveGridPoint(point.giveMapPoint().X, point.giveMapPoint().Y + 1);
                        normalpoint2 = playerDistanceGrid[point.giveMapPoint().X, point.giveMapPoint().Y + 1];
                        if (point2.isAccicible() && normalpoint2.Distance > normalPoint.Distance + 2)
                        {
                            normalpoint2.Distance = normalPoint.Distance + 2;
                            normalpoint2.Dir = 2;
                            normalpoint2.Prev = normalPoint;
                            q.Enqueue(point2);
                        }
                    }
                    if ((point.giveMapPoint().X + 1) < Constant.MAP_SIZE)
                    {
                        point2 = gHandler.giveGridPoint(point.giveMapPoint().X + 1, point.giveMapPoint().Y);
                        normalpoint2 = playerDistanceGrid[point.giveMapPoint().X + 1, point.giveMapPoint().Y];
                        if (point2.isAccicible() && normalpoint2.Distance > normalPoint.Distance + 2)
                        {
                            normalpoint2.Distance = normalPoint.Distance + 2;
                            normalpoint2.Dir = 1;
                            normalpoint2.Prev = normalPoint;
                            q.Enqueue(point2);
                        }
                    }
                    if ((point.giveMapPoint().X - 1) >= 0)
                    {
                        point2 = gHandler.giveGridPoint(point.giveMapPoint().X - 1, point.giveMapPoint().Y);
                        normalpoint2 = playerDistanceGrid[point.giveMapPoint().X - 1, point.giveMapPoint().Y];
                        if (point2.isAccicible() && normalpoint2.Distance > normalPoint.Distance + 2)
                        {
                            normalpoint2.Distance = normalPoint.Distance + 2;
                            normalpoint2.Dir = 3;
                            normalpoint2.Prev = normalPoint;
                            q.Enqueue(point2);
                        }
                    }
                }
                else if (normalPoint.Dir == 2)
                {
                    if ((point.giveMapPoint().Y + 1) < Constant.MAP_SIZE)
                    {
                        point2 = gHandler.giveGridPoint(point.giveMapPoint().X, point.giveMapPoint().Y + 1);
                        normalpoint2 = playerDistanceGrid[point.giveMapPoint().X, point.giveMapPoint().Y + 1];
                        if (point2.isAccicible() && normalpoint2.Distance > normalPoint.Distance + 1)
                        {
                            normalpoint2.Distance = normalPoint.Distance + 1;
                            normalpoint2.Dir = normalPoint.Dir;
                            normalpoint2.Prev = normalPoint;
                            q.Enqueue(point2);
                        }
                    }
                    if ((point.giveMapPoint().Y - 1) >= 0)
                    {
                        point2 = gHandler.giveGridPoint(point.giveMapPoint().X, point.giveMapPoint().Y - 1);
                        normalpoint2 = playerDistanceGrid[point.giveMapPoint().X, point.giveMapPoint().Y - 1];
                        if (point2.isAccicible() && normalpoint2.Distance > normalPoint.Distance + 2)
                        {
                            normalpoint2.Distance = normalPoint.Distance + 2;
                            normalpoint2.Dir = 0;
                            normalpoint2.Prev = normalPoint;
                            q.Enqueue(point2);
                        }
                    }
                    if ((point.giveMapPoint().X + 1) < Constant.MAP_SIZE)
                    {
                        point2 = gHandler.giveGridPoint(point.giveMapPoint().X + 1, point.giveMapPoint().Y);
                        normalpoint2 = playerDistanceGrid[point.giveMapPoint().X + 1, point.giveMapPoint().Y];
                        if (point2.isAccicible() && normalpoint2.Distance > normalPoint.Distance + 2)
                        {
                            normalpoint2.Distance = normalPoint.Distance + 2;
                            normalpoint2.Dir = 1;
                            normalpoint2.Prev = normalPoint;
                            q.Enqueue(point2);
                        } 
                    }
                    if ((point.giveMapPoint().X - 1) >= 0)
                    {
                        point2 = gHandler.giveGridPoint(point.giveMapPoint().X - 1, point.giveMapPoint().Y);
                        normalpoint2 = playerDistanceGrid[point.giveMapPoint().X - 1, point.giveMapPoint().Y];
                        if (point2.isAccicible() && normalpoint2.Distance > normalPoint.Distance + 2)
                        {
                            normalpoint2.Distance = normalPoint.Distance + 2;
                            normalpoint2.Dir = 3;
                            normalpoint2.Prev = normalPoint;
                            q.Enqueue(point2);
                        }
                    }
                }
                else if (normalPoint.Dir == 1)
                {
                    if ((point.giveMapPoint().X + 1) < Constant.MAP_SIZE)
                    {
                        point2 = gHandler.giveGridPoint(point.giveMapPoint().X + 1, point.giveMapPoint().Y);
                        normalpoint2 = playerDistanceGrid[point.giveMapPoint().X + 1, point.giveMapPoint().Y];
                        if (point2.isAccicible() && normalpoint2.Distance > normalPoint.Distance + 1)
                        {
                            normalpoint2.Distance = normalPoint.Distance + 1;
                            normalpoint2.Dir = normalPoint.Dir;
                            normalpoint2.Prev = normalPoint;
                            q.Enqueue(point2);
                        }
                    }
                    if ((point.giveMapPoint().Y + 1) < Constant.MAP_SIZE)
                    {
                        point2 = gHandler.giveGridPoint(point.giveMapPoint().X, point.giveMapPoint().Y + 1);
                        normalpoint2 = playerDistanceGrid[point.giveMapPoint().X, point.giveMapPoint().Y + 1];
                        if (point2.isAccicible() && normalpoint2.Distance > normalPoint.Distance + 2)
                        {
                            normalpoint2.Distance = normalPoint.Distance + 2;
                            normalpoint2.Dir = 2;
                            normalpoint2.Prev = normalPoint;
                            q.Enqueue(point2);
                        }
                    }
                    if ((point.giveMapPoint().Y - 1) >= 0)
                    {
                        point2 = gHandler.giveGridPoint(point.giveMapPoint().X, point.giveMapPoint().Y - 1);
                        normalpoint2 = playerDistanceGrid[point.giveMapPoint().X, point.giveMapPoint().Y - 1];
                        if (point2.isAccicible() && normalpoint2.Distance > normalPoint.Distance + 2)
                        {
                            normalpoint2.Distance = normalPoint.Distance + 2;
                            normalpoint2.Dir = 0;
                            normalpoint2.Prev = normalPoint;
                            q.Enqueue(point2);
                        }
                    }
                    if ((point.giveMapPoint().X - 1) >= 0)
                    {
                        point2 = gHandler.giveGridPoint(point.giveMapPoint().X - 1, point.giveMapPoint().Y);
                        normalpoint2 = playerDistanceGrid[point.giveMapPoint().X - 1, point.giveMapPoint().Y];
                        if (point2.isAccicible() && normalpoint2.Distance > normalPoint.Distance + 2)
                        {
                            normalpoint2.Distance = normalPoint.Distance + 2;
                            normalpoint2.Dir = 3;
                            normalpoint2.Prev = normalPoint;
                            q.Enqueue(point2);
                        }
                    }
                }
                else if (normalPoint.Dir == 3)
                {
                    if ((point.giveMapPoint().X - 1) >= 0)
                    {
                        point2 = gHandler.giveGridPoint(point.giveMapPoint().X - 1, point.giveMapPoint().Y);
                        normalpoint2 = playerDistanceGrid[point.giveMapPoint().X - 1, point.giveMapPoint().Y];
                        if (point2.isAccicible() && normalpoint2.Distance > normalPoint.Distance + 1)
                        {
                            normalpoint2.Distance = normalPoint.Distance + 1;
                            normalpoint2.Dir = normalPoint.Dir;
                            normalpoint2.Prev = normalPoint;
                            q.Enqueue(point2);
                        }
                    }
                    if ((point.giveMapPoint().X + 1) < Constant.MAP_SIZE)
                    {
                        point2 = gHandler.giveGridPoint(point.giveMapPoint().X + 1, point.giveMapPoint().Y);
                        normalpoint2 = playerDistanceGrid[point.giveMapPoint().X + 1, point.giveMapPoint().Y];
                        if (point2.isAccicible() && normalpoint2.Distance > normalPoint.Distance + 2)
                        {
                            normalpoint2.Distance = normalPoint.Distance + 2;
                            normalpoint2.Dir = 1;
                            normalpoint2.Prev = normalPoint;
                            q.Enqueue(point2);
                        }
                    }
                    if ((point.giveMapPoint().Y + 1) < Constant.MAP_SIZE)
                    {
                        point2 = gHandler.giveGridPoint(point.giveMapPoint().X, point.giveMapPoint().Y + 1);
                        normalpoint2 = playerDistanceGrid[point.giveMapPoint().X, point.giveMapPoint().Y + 1];
                        if (point2.isAccicible() && normalpoint2.Distance > normalPoint.Distance + 2)
                        {
                            normalpoint2.Distance = normalPoint.Distance + 2;
                            normalpoint2.Dir = 2;
                            normalpoint2.Prev = normalPoint;
                            q.Enqueue(point2);
                        }
                    }
                    if ((point.giveMapPoint().Y - 1) >= 0)
                    {
                        point2 = gHandler.giveGridPoint(point.giveMapPoint().X, point.giveMapPoint().Y - 1);
                        normalpoint2 = playerDistanceGrid[point.giveMapPoint().X, point.giveMapPoint().Y - 1];
                        if (point2.isAccicible() && normalpoint2.Distance > normalPoint.Distance + 2)
                        {
                            normalpoint2.Distance = normalPoint.Distance + 2;
                            normalpoint2.Dir = 0;
                            normalpoint2.Prev = normalPoint;
                            q.Enqueue(point2);
                        }
                    }
                }                
            }
        }
        /*
        private Player playerPosition(String command)
        {
            Player p = player.myclone();
            if (command == Constant.UP)
            {
                if (p.Direction == 0)
                {
                    if (p.getCurrentP().Y != 0)
                    {
                        p.setCurrentP(new System.Drawing.Point(p.getCurrentP().X, p.getCurrentP().Y - 1));
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    p.Direction = 0;
                }
            }
            if (command == Constant.DOWN)
            {
                if (p.Direction == 2)
                {
                    if (p.getCurrentP().Y != 9)
                    {
                        p.setCurrentP(new System.Drawing.Point(p.getCurrentP().X, p.getCurrentP().Y + 1));
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    p.Direction = 2;
                }
            }
            if (command == Constant.LEFT)
            {
                if (p.Direction == 1)
                {
                    if (p.getCurrentP().X != 9)
                    {
                        p.setCurrentP(new System.Drawing.Point(p.getCurrentP().X + 1, p.getCurrentP().Y));
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    p.Direction = 1;
                }
            }
            if (command == Constant.RIGHT)
            {
                if (p.Direction == 3)
                {
                    if (p.getCurrentP().X != 0)
                    {
                        p.setCurrentP(new System.Drawing.Point(p.getCurrentP().X - 1, p.getCurrentP().Y));
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    p.Direction = 3;
                }
            }
            return p;
        }
        */
        /*
        public int giveNextHeuristicValue(String command)
        {
            try
            {
                Player p = playerPosition(command);
                calcDistances(p);
                processNearestTarget();
                Console.WriteLine("---------------------distance heuristic for:" + command);
                printPlayerGrid();
                int dist = nearestTarget.Distance;
                return (dist);
            }
            catch (Exception e)
            {
                return int.MaxValue;
            }
        }
         */
        private void processNearestTarget()
        {
            calcDistances();
            printPlayerGrid();
            nearestTarget = new GridMap.NormalDistanceGridPoint(new System.Drawing.Point(-1, -1));
            nearestTarget.Distance = int.MaxValue;
            for (int i = 0; i < gHandler.giveCoinList().Count; i++)
            {
                GridMap.Coin c = gHandler.giveCoinList()[i];
                GridMap.NormalDistanceGridPoint np= playerDistanceGrid[c.giveMapPoint().X, c.giveMapPoint().Y];
                if (nearestTarget.Distance > np.Distance && np.Distance < c.Time)
                {
                    nearestTarget = np;
                }
            }
            if (nearestTarget.giveMapPoint().X != -1 && player.Health>=100)
                return;
            for (int i = 0; i < gHandler.giveLifepackList().Count; i++)
            {
                GridMap.LifePack l = gHandler.giveLifepackList()[i];
                GridMap.NormalDistanceGridPoint np = playerDistanceGrid[l.giveMapPoint().X, l.giveMapPoint().Y];
                if (nearestTarget.Distance > np.Distance && np.Distance < l.Time)
                {
                    nearestTarget = np;
                }
            }
        }

        public GridMap.NormalDistanceGridPoint giveNearestTarget()
        {
            processNearestTarget();
            return nearestTarget;
        }
        public void printPlayerGrid()
        {
            for (int i = 0; i < Constant.MAP_SIZE; i++)
            {
                for (int j = 0; j < Constant.MAP_SIZE; j++)
                {                    
                    if (playerDistanceGrid[j, i].Distance != int.MaxValue)
                    {
                        Console.Write(playerDistanceGrid[j, i].Distance);
                        /*if(playerDistanceGrid[j, i].Prev!=null)
                            Console.Write("," + playerDistanceGrid[j, i].Prev.giveMapPointString()+"\t");
                        else
                         */
                            Console.Write("\t");                         
                    }
                    else
                    {
                        Console.Write("vv\t");
                    }                                        
                }
                Console.WriteLine("");
            }
            Console.WriteLine("-------------------------------------");
        }
    }
}
