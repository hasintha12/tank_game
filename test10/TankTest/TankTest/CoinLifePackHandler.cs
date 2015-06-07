using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TankTest
{
    class CoinLifePackHandler
    {
        private List<GridMap.Coin> coinList;
        private List<GridMap.LifePack> lifepackList;
        private NetworkConnection conn;
        private MapBoxPoint[,] grid;
        private PlayerHandler pHandler;
        public CoinLifePackHandler(NetworkConnection con,MapBoxPoint[,] grid,PlayerHandler pHandler)
        {
            coinList = new List<GridMap.Coin>();
            lifepackList = new List<GridMap.LifePack>();
            this.conn = con;
            this.grid = grid;
            this.pHandler = pHandler;

        }
        public void AddTankCoinPile(System.Drawing.Point point,int val){
            GridMap.Coin cc = new GridMap.Coin(point, val,int.MaxValue);
                        
            grid[point.X,point.Y] = cc;
            coinList.Add(cc);                
        }
        public void addCoin()
        {
            String coin = conn.giveLastCoin();
            if (coin != null)
            {
                //add coin
                String pos, lt, val;
                pos = coin.Split(':')[1];
                lt = coin.Split(':')[2];
                val = coin.Split(':')[3];
                val = val.Substring(0, val.Length - 1);
                int x, y, lti, vali;
                x = int.Parse(pos.Split(',')[0]);
                y = int.Parse(pos.Split(',')[1]);
                lti = int.Parse(lt);
                vali = int.Parse(val);
                GridMap.Coin cc = new GridMap.Coin(new System.Drawing.Point(x, y), vali, lti);
                if (((GridMap.NormalPoint)grid[x, y]).isOccupied())
                {
                    cc.setOccupied();
                }
                grid[x, y] = cc;
                coinList.Add(cc);                
            }
        }
        public void addLifePack()
        {
            String life = conn.giveLastLifePack();
            if (life != null)
            {
                //add life to grid
                String pos, lt;
                pos = life.Split(':')[1];
                lt = life.Split(':')[2];
                lt = lt.Substring(0, lt.Length - 1);
                int x, y, lti;
                x = int.Parse(pos.Split(',')[0]);
                y = int.Parse(pos.Split(',')[1]);
                lti = int.Parse(lt);
                GridMap.LifePack lp = new GridMap.LifePack(new System.Drawing.Point(x, y), lti);                
                if (((GridMap.NormalPoint)grid[x, y]).isOccupied())
                {
                    lp.setOccupied();
                }
                grid[x, y] = lp;
                lifepackList.Add(lp);
            }
        }
        public void process()
        {
            while (true)
            {
                
                for (int i = 0; i < coinList.Count; i++)
                {
                    GridMap.Coin coin = coinList[i];
                    if(pHandler.playerOnPoint(coin.giveMapPoint())){
                        coinList.Remove(coin);
                        i--;
                        GridMap.NormalPoint cc = new GridMap.NormalPoint(coin.giveMapPoint());
                        if (((GridMap.NormalPoint)grid[coin.giveMapPoint().X, coin.giveMapPoint().Y]).isOccupied())
                        {
                            cc.setOccupied();
                        }
                        grid[coin.giveMapPoint().X, coin.giveMapPoint().Y] = cc;
                    }
                    if (coin.Time > Constant.COINLIFE_REFRESHDELAY)
                    {
                        coin.reduceTime();
                    }
                    else
                    {
                        coinList.Remove(coin);
                        i--;
                        GridMap.NormalPoint cc = new GridMap.NormalPoint(coin.giveMapPoint());
                        if (((GridMap.NormalPoint)grid[coin.giveMapPoint().X, coin.giveMapPoint().Y]).isOccupied())
                        {
                            cc.setOccupied();
                        }
                        grid[coin.giveMapPoint().X, coin.giveMapPoint().Y]=cc;
                    }
                }

                for (int i = 0; i < lifepackList.Count;i++)
                {
                    GridMap.LifePack lp = lifepackList[i];
                    if (pHandler.playerOnPoint(lp.giveMapPoint()))
                    {
                        lifepackList.Remove(lp);
                        i--;
                        GridMap.NormalPoint cc = new GridMap.NormalPoint(lp.giveMapPoint());//(GridMap.NormalPoint)grid[coin.giveMapPoint().X, coin.giveMapPoint().Y];
                        if (((GridMap.NormalPoint)grid[lp.giveMapPoint().X, lp.giveMapPoint().Y]).isOccupied())
                        {
                            cc.setOccupied();
                        }
                        grid[lp.giveMapPoint().X, lp.giveMapPoint().Y] = cc;
                    }
                    if (lp.Time > Constant.COINLIFE_REFRESHDELAY)
                    {
                        lp.reduceTime();
                    }
                    else
                    {
                        lifepackList.Remove(lp);
                        i--;
                        GridMap.NormalPoint cc=new GridMap.NormalPoint(lp.giveMapPoint()); //(GridMap.NormalPoint)grid[lp.giveMapPoint().X, lp.giveMapPoint().Y];
                        if (((GridMap.NormalPoint)grid[lp.giveMapPoint().X, lp.giveMapPoint().Y]).isOccupied())
                        {
                            cc.setOccupied();
                        }
                        grid[lp.giveMapPoint().X, lp.giveMapPoint().Y] = cc;
                    }
                }
                addCoin();
                addLifePack();
                Thread.Sleep(Constant.COINLIFE_REFRESHDELAY);
            }
        }

        public List<GridMap.Coin> giveCoinList()
        {
            return coinList;
        }
        public List<GridMap.LifePack> giveLifePackList()
        {
            return lifepackList;
        }
    }
}
