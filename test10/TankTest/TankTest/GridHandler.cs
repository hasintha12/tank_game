using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TankTest
{
    class GridHandler
    {
        private MapBoxPoint[,] grid;
        private NetworkConnection conn;
        private PlayerHandler pHandler;
        private CoinLifePackHandler clHandler;
        private Thread coinLifepackThread;
        public GridHandler(NetworkConnection con)
        {
            conn = con;
            grid=new MapBoxPoint[Constant.MAP_SIZE,Constant.MAP_SIZE];
            pHandler = new PlayerHandler(conn);
            clHandler = new CoinLifePackHandler(conn, grid,pHandler);
            coinLifepackThread = new Thread(new ThreadStart(clHandler.process));
        }
        public void loadGrid()
        {
            pHandler.loadPlayers();
            for (int i = 0; i < Constant.MAP_SIZE; i++)
            {
                for (int j = 0; j < Constant.MAP_SIZE; j++)
                {
                    grid[i, j] = new GridMap.NormalPoint(new System.Drawing.Point(i, j));
                }
            }
            String imsg = conn.giveIMsg();
            String brickstring = imsg.Split(':')[2];
            for (int i = 0; i < brickstring.Split(';').Length; i++)
            {
                String bPoint = brickstring.Split(';')[i];
                int x, y;
                x = int.Parse(bPoint.Split(',')[0]);
                y = int.Parse(bPoint.Split(',')[1]);
                grid[x, y] = new GridMap.Brick(new System.Drawing.Point(x,y));                
            }
            String stonestring = imsg.Split(':')[3];
            for (int i = 0; i < stonestring.Split(';').Length; i++)
            {
                String sPoint = stonestring.Split(';')[i];
                int x, y;
                x = int.Parse(sPoint.Split(',')[0]);
                y = int.Parse(sPoint.Split(',')[1]);
                grid[x, y] = new GridMap.Stone(new System.Drawing.Point(x, y));
            }
            String waterstring = imsg.Split(':')[4];
            waterstring = waterstring.Substring(0, waterstring.Length - 1);
            for (int i = 0; i < waterstring.Split(';').Length; i++)
            {
                String wPoint = waterstring.Split(';')[i];
                int x, y;
                x = int.Parse(wPoint.Split(',')[0]);
                y = int.Parse(wPoint.Split(',')[1]);
                grid[x, y] = new GridMap.Water(new System.Drawing.Point(x, y));
            }
            for (int i = 0; i < pHandler.givePlayersCount(); i++)
            {
                GridMap.NormalPoint np = (GridMap.NormalPoint)grid[pHandler.givePlayer(i).getCurrentP().X, pHandler.givePlayer(i).getCurrentP().Y];
                np.setOccupied();
                grid[pHandler.givePlayer(i).getCurrentP().X, pHandler.givePlayer(i).getCurrentP().Y] = np;
            }
            coinLifepackThread.Start();                
        }    
        public void updateGrid()    // update the grid from Gmsgs
        {
            pHandler.updatePlayerData();
            String gmsg = conn.giveLastGmsg();
            if (gmsg != null)
            {
                String brickString = gmsg.Split(':')[gmsg.Split(':').Length - 1];
                brickString = brickString.Substring(0, brickString.Length - 1);
                for (int i = 0; i < brickString.Split(';').Length; i++)
                {
                    String brick = brickString.Split(';')[i];
                    int x, y, dam;
                    x = int.Parse(brick.Split(',')[0]);
                    y =int.Parse(brick.Split(',')[1]);
                    dam = int.Parse(brick.Split(',')[2]);
                    GridMap.Brick b =(GridMap.Brick) grid[x, y];
                    b.setDamageLevel(dam);
                    grid[x, y] = b;
                }
            }
            for (int i = 0; i < pHandler.givePlayersCount(); i++)
            {
                Player p = pHandler.givePlayer(i);
                if (p.Health>0)
                {
                    GridMap.NormalPoint np = (GridMap.NormalPoint)grid[p.getCurrentP().X, p.getCurrentP().Y];
                    np.setOccupied();
                    grid[p.getCurrentP().X, p.getCurrentP().Y] = np;
                    if (p.PrevP != p.getCurrentP())
                    {
                        GridMap.NormalPoint np2 = (GridMap.NormalPoint)grid[p.PrevP.X, p.PrevP.Y];
                        np2.setUnoccupied();
                        grid[p.PrevP.X, p.PrevP.Y] = np2;
                    }
                }
                else
                {
                    if (!(grid[p.getCurrentP().X, p.getCurrentP().Y] is GridMap.Water))
                    {
                        GridMap.NormalPoint np = (GridMap.NormalPoint)grid[p.getCurrentP().X, p.getCurrentP().Y];
                        if (!p.isCoinDropped())
                        {
                            clHandler.AddTankCoinPile(p.getCurrentP(), p.Coins);//When crahed coin pile should drop
                            p.setCoinDropped();
                        }
                        np.setUnoccupied();
                        grid[p.getCurrentP().X, p.getCurrentP().Y] = np;                        
                        if (p.PrevP != p.getCurrentP())
                        {
                            GridMap.NormalPoint np2 = (GridMap.NormalPoint)grid[p.PrevP.X, p.PrevP.Y];
                            np2.setUnoccupied();
                            grid[p.PrevP.X, p.PrevP.Y] = np2;
                        }
                    }
                }
            }                              
        }
        public void printGrid()
        {
            for (int i = 0; i < Constant.MAP_SIZE; i++)
            {
                for (int j = 0; j < Constant.MAP_SIZE; j++)
                {
                    /*if (grid[j, i].Prev != null)
                        Console.Write(grid[j, i].Prev.giveMapPoint().X + "," + grid[j, i].Prev.giveMapPoint().Y + "-");
                    else
                        Console.Write("-,--");
                    if (grid[j, i].Distance != int.MaxValue)
                    {
                        Console.Write(grid[j, i].Distance);
                    }
                    else
                    {
                        Console.Write("vv");
                    }*/
                    if (grid[j, i] is GridMap.NormalPoint)
                    {
                        GridMap.NormalPoint np=(GridMap.NormalPoint)grid[j,i];
                        if (np.isOccupied())
                        {
                            /*if(np.givePlayer().Name==pHandler.giveMyPlayer().Name)
                                Console.Write("m"+pHandler.giveMyPlayer().Name);
                            else*/
                            Console.Write("Player");
                        }
                    }                    
                    if (grid[j, i] is GridMap.NormalPoint)
                    {
                        if (grid[j, i] is GridMap.Brick)
                            Console.Write("B\t");
                        else if (grid[j, i] is GridMap.Coin)
                            Console.Write("C\t");
                        else if (grid[j, i] is GridMap.LifePack)
                            Console.Write("L\t");
                        else 
                            Console.Write("N\t");
                    }
                    if (grid[j, i] is GridMap.Stone)
                        Console.Write("S\t");
                    if (grid[j, i] is GridMap.Water)
                        Console.Write("W\t");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("-------------------------------------");
        }

        public PlayerHandler givePlayerHandler()  //to get the player data to tank brain
        {
            return pHandler;
        }
        public MapBoxPoint giveGridPoint(int x,int y)
        {
            return grid[x,y];
        }
        public List<GridMap.Coin> giveCoinList()
        {
            return clHandler.giveCoinList();
        }
        public List<GridMap.LifePack> giveLifepackList()
        {
            return clHandler.giveLifePackList();
        }
    }
}
