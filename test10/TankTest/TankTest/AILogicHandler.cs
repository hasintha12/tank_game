using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TankTest
{
    class AILogicHandler
    {
        private GridHandler gHandler;
        private ErrorHandler eHandler;
        private Player[] players;
        private PlayerAILogicHandler playerLogic;
        private int myPlayerIndex;

        GridMap.NormalDistanceGridPoint nearestTarget;
        Player me;

        public AILogicHandler(GridHandler g, ErrorHandler e)
        {
            gHandler = g;
            eHandler = e;
        }

        public void loadPlayers()
        {
            players = new Player[gHandler.givePlayerHandler().givePlayersCount()];

            for (int i = 0; i < gHandler.givePlayerHandler().givePlayersCount(); i++)
            {
                players[i] = (gHandler.givePlayerHandler().givePlayer(i));
            }
            playerLogic = new PlayerAILogicHandler(gHandler, players[myPlayerIndex]);
            myPlayerIndex = gHandler.givePlayerHandler().giveMyPlayerIndex();
            me = players[myPlayerIndex];
        }

        public String giveNextCommand()
        {
            String command = Constant.STOP;
            gHandler.updateGrid();
            gHandler.printGrid();
            /////
            command = processNextCommand();
            ////
            return command;
        }

        private String processNextCommand()
        {
            if (targeted())
            {
                //if I'm targeted have to move
                return giveRandomMove();
            }
            if (processShoot2())
                return Constant.SHOOT;
            nearestTarget = playerLogic.giveNearestTarget().Clone();
            Console.WriteLine("nearestTarget============" + (int)(nearestTarget.giveMapPoint().X * Constant.MAP_SIZE + nearestTarget.giveMapPoint().Y));
            if (nearestTarget.giveMapPoint() != me.getCurrentP())
            {
                Console.WriteLine("target path:");
                while (nearestTarget.Prev != null && nearestTarget.Prev.giveMapPoint() != me.getCurrentP())
                {
                    Console.Write(((int)nearestTarget.giveMapPoint().X * Constant.MAP_SIZE + (int)nearestTarget.giveMapPoint().Y) + ", ");
                    nearestTarget = nearestTarget.Prev;
                }
                Console.WriteLine("\nme at===================" + (int)(me.getCurrentP().X * Constant.MAP_SIZE + me.getCurrentP().Y));
                Console.WriteLine("next cell=================" + (int)(nearestTarget.giveMapPoint().X * Constant.MAP_SIZE + nearestTarget.giveMapPoint().Y));
                int targetDir = 0;
                if (nearestTarget.giveMapPoint().X >= 0 && nearestTarget.giveMapPoint().Y >= 0 && nearestTarget.giveMapPoint().X < Constant.MAP_SIZE && nearestTarget.giveMapPoint().Y < Constant.MAP_SIZE)
                {
                    if (nearestTarget.giveMapPoint().X > me.getCurrentP().X)
                        targetDir = 1;
                    if (nearestTarget.giveMapPoint().X < me.getCurrentP().X)
                        targetDir = 3;
                    if (nearestTarget.giveMapPoint().Y > me.getCurrentP().Y)
                        targetDir = 2;

                    if (targetDir == 0)
                        return Constant.UP;
                    if (targetDir == 1)
                        return Constant.RIGHT;
                    if (targetDir == 2)
                        return Constant.DOWN;
                    if (targetDir == 3)
                        return Constant.LEFT;
                }
                else
                {
                    //as a secondary shoot option tank get rotated
                    /*int dir = me.Direction;
                    if (dir == 0)
                        return Constant.RIGHT;
                    if (dir == 1)
                        return Constant.DOWN;
                    if (dir == 2)
                        return Constant.LEFT;
                    if (dir == 3)
                        return Constant.UP;*/
                    return giveRandomMove();
                }
            }
                return Constant.STOP;//default
            
        }

        private bool targeted()
        {
            if (gHandler.givePlayerHandler().isTargeted())
            {
                return true;
            }
            return false;
        }
        private Boolean processShoot2()
        {
            if (eHandler.giveMovingShootingError() == Constant.S2C_CELLOCCUPIED)
                return true;
            if (me.Direction == 0)
            {
                for (int i = me.getCurrentP().Y - 1; i >= 0; i--)
                {
                    if ((gHandler.giveGridPoint(me.getCurrentP().X, i) is GridMap.Stone))//|| (gHandler.giveGridPoint(me.getCurrentP().X, i) is GridMap.Coin) || (gHandler.giveGridPoint(me.getCurrentP().X, i) is GridMap.LifePack))
                        break;
                    if (gHandler.giveGridPoint(me.getCurrentP().X, i) is GridMap.NormalPoint)
                    {
                        GridMap.NormalPoint np = (GridMap.NormalPoint)(gHandler.giveGridPoint(me.getCurrentP().X, i));
                        if (np.isOccupied())
                        {
                            if (gHandler.givePlayerHandler().playerDirOnPoint(np.giveMapPoint()) == 0 || gHandler.givePlayerHandler().playerDirOnPoint(np.giveMapPoint()) == 2)
                                return true;
                        }
                        if (gHandler.giveGridPoint(me.getCurrentP().X, i) is GridMap.Brick)
                        {
                            GridMap.Brick bb = (GridMap.Brick)(gHandler.giveGridPoint(me.getCurrentP().X, i));
                            if (bb.getDamageLevel() < 4)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            if (me.Direction == 2)
            {
                for (int i = me.getCurrentP().Y + 1; i < Constant.MAP_SIZE; i++)
                {
                    if ((gHandler.giveGridPoint(me.getCurrentP().X, i) is GridMap.Stone))//|| (gHandler.giveGridPoint(me.getCurrentP().X, i) is GridMap.Coin) || (gHandler.giveGridPoint(me.getCurrentP().X, i) is GridMap.LifePack))
                        break;
                    if (gHandler.giveGridPoint(me.getCurrentP().X, i) is GridMap.NormalPoint)
                    {
                        GridMap.NormalPoint np = (GridMap.NormalPoint)(gHandler.giveGridPoint(me.getCurrentP().X, i));
                        if (np.isOccupied())
                        {
                            if (gHandler.givePlayerHandler().playerDirOnPoint(np.giveMapPoint()) == 0 || gHandler.givePlayerHandler().playerDirOnPoint(np.giveMapPoint()) == 2)
                            return true;
                        }
                        if (gHandler.giveGridPoint(me.getCurrentP().X, i) is GridMap.Brick)
                        {
                            GridMap.Brick bb = (GridMap.Brick)(gHandler.giveGridPoint(me.getCurrentP().X, i));
                            if (bb.getDamageLevel() < 4)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            if (me.Direction == 1)
            {
                for (int i = me.getCurrentP().X + 1; i < Constant.MAP_SIZE; i++)
                {
                    if ((gHandler.giveGridPoint(i, me.getCurrentP().Y) is GridMap.Stone))//|| (gHandler.giveGridPoint(me.getCurrentP().X, i) is GridMap.Coin) || (gHandler.giveGridPoint(me.getCurrentP().X, i) is GridMap.LifePack))
                        break;
                    if (gHandler.giveGridPoint(i, me.getCurrentP().Y) is GridMap.NormalPoint)
                    {
                        GridMap.NormalPoint np = (GridMap.NormalPoint)(gHandler.giveGridPoint(i, me.getCurrentP().Y));
                        if (np.isOccupied())
                        {
                            if (gHandler.givePlayerHandler().playerDirOnPoint(np.giveMapPoint()) == 1 || gHandler.givePlayerHandler().playerDirOnPoint(np.giveMapPoint()) == 3)
                            return true;
                        }
                        if (gHandler.giveGridPoint(i,me.getCurrentP().Y) is GridMap.Brick)
                        {
                            GridMap.Brick bb = (GridMap.Brick)(gHandler.giveGridPoint(i,me.getCurrentP().Y));
                            if (bb.getDamageLevel() < 4)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            if (me.Direction == 3)
            {
                for (int i = me.getCurrentP().X - 1; i >= 0; i--)
                {
                    if ((gHandler.giveGridPoint(i, me.getCurrentP().Y) is GridMap.Stone))//|| (gHandler.giveGridPoint(me.getCurrentP().X, i) is GridMap.Coin) || (gHandler.giveGridPoint(me.getCurrentP().X, i) is GridMap.LifePack))
                        break;
                    if (gHandler.giveGridPoint(i, me.getCurrentP().Y) is GridMap.NormalPoint)
                    {
                        GridMap.NormalPoint np = (GridMap.NormalPoint)(gHandler.giveGridPoint(i, me.getCurrentP().Y));
                        if (np.isOccupied())
                        {
                            if (gHandler.givePlayerHandler().playerDirOnPoint(np.giveMapPoint()) == 1 || gHandler.givePlayerHandler().playerDirOnPoint(np.giveMapPoint()) == 3)
                            return true;
                        }
                        if (gHandler.giveGridPoint(i,me.getCurrentP().Y) is GridMap.Brick)
                        {
                            GridMap.Brick bb = (GridMap.Brick)(gHandler.giveGridPoint(i,me.getCurrentP().Y));
                            if (bb.getDamageLevel() < 4)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private Boolean processShoot()
        {
            if (eHandler.giveMovingShootingError() == Constant.S2C_CELLOCCUPIED)
                return true;
            if (me.Direction == 0)
            {
                for (int i = me.getCurrentP().Y - 1; i >= 0; i--)
                {
                    if (me.getCurrentP().X + ((int)i / 3) < Constant.MAP_SIZE)
                    {
                        if ((gHandler.giveGridPoint(me.getCurrentP().X + ((int)i / 3), i) is GridMap.NormalPoint))
                        {
                            GridMap.NormalPoint np = (GridMap.NormalPoint)(gHandler.giveGridPoint(me.getCurrentP().X + ((int)i / 3), i));
                            if (np.isOccupied())// && np.givePlayer().Direction == 3)
                            {
                                return true;
                            }
                        }
                    }
                    if (me.getCurrentP().X - ((int)i / 3) >= 0)
                    {
                        if ((gHandler.giveGridPoint(me.getCurrentP().X - ((int)i / 3), i) is GridMap.NormalPoint))
                        {
                            GridMap.NormalPoint np = (GridMap.NormalPoint)(gHandler.giveGridPoint(me.getCurrentP().X - ((int)i / 3), i));
                            if (np.isOccupied())// && np.givePlayer().Direction == 1)
                            {
                                return true;
                            }
                        }
                    }
                    if ((gHandler.giveGridPoint(me.getCurrentP().X, i) is GridMap.Stone))//|| (gHandler.giveGridPoint(me.getCurrentP().X, i) is GridMap.Coin) || (gHandler.giveGridPoint(me.getCurrentP().X, i) is GridMap.LifePack))
                        break;
                    if (gHandler.giveGridPoint(me.getCurrentP().X, i) is GridMap.Brick)
                    {
                        GridMap.Brick b = (GridMap.Brick)gHandler.giveGridPoint(me.getCurrentP().X, i);
                        if (!b.isAccicible())
                            return true;
                    }
                }
            }
            if (me.Direction == 2)
            {
                for (int i = me.getCurrentP().Y + 1; i < Constant.MAP_SIZE; i++)
                {
                    if (me.getCurrentP().X + ((int)i / 3) < Constant.MAP_SIZE)
                    {
                        if ((gHandler.giveGridPoint(me.getCurrentP().X + ((int)i / 3), i) is GridMap.NormalPoint))
                        {
                            GridMap.NormalPoint np = (GridMap.NormalPoint)(gHandler.giveGridPoint(me.getCurrentP().X + ((int)i / 3), i));
                            if (np.isOccupied())// && np.givePlayer().Direction == 3)
                            {
                                return true;
                            }
                        }
                    }
                    if (me.getCurrentP().X - ((int)i / 3) >= 0)
                    {
                        if ((gHandler.giveGridPoint(me.getCurrentP().X - ((int)i / 3), i) is GridMap.NormalPoint))
                        {
                            GridMap.NormalPoint np = (GridMap.NormalPoint)(gHandler.giveGridPoint(me.getCurrentP().X - ((int)i / 3), i));
                            if (np.isOccupied())// && np.givePlayer().Direction == 1)
                            {
                                return true;
                            }
                        }
                    }
                    if ((gHandler.giveGridPoint(me.getCurrentP().X, i) is GridMap.Stone))//|| (gHandler.giveGridPoint(me.getCurrentP().X, i) is GridMap.Coin) || (gHandler.giveGridPoint(me.getCurrentP().X, i) is GridMap.LifePack))
                        break;
                    if (gHandler.giveGridPoint(me.getCurrentP().X, i) is GridMap.Brick)
                    {
                        GridMap.Brick b = (GridMap.Brick)gHandler.giveGridPoint(me.getCurrentP().X, i);
                        if (!b.isAccicible())
                            return true;
                    }
                }
            }
            if (me.Direction == 1)
            {
                for (int i = me.getCurrentP().X + 1; i < Constant.MAP_SIZE; i++)
                {
                    if (me.getCurrentP().Y + ((int)i / 3) < Constant.MAP_SIZE)
                    {
                        if ((gHandler.giveGridPoint(i, me.getCurrentP().Y + ((int)i / 3)) is GridMap.NormalPoint))
                        {
                            GridMap.NormalPoint np = (GridMap.NormalPoint)(gHandler.giveGridPoint(i, me.getCurrentP().Y + ((int)i / 3)));
                            if (np.isOccupied())// && np.givePlayer().Direction == 0)
                            {
                                return true;
                            }
                        }
                    }
                    if (me.getCurrentP().Y - ((int)i / 3) >= 0)
                    {
                        if ((gHandler.giveGridPoint(i, me.getCurrentP().Y - ((int)i / 3)) is GridMap.NormalPoint))
                        {
                            GridMap.NormalPoint np = (GridMap.NormalPoint)(gHandler.giveGridPoint(i, me.getCurrentP().Y - ((int)i / 3)));
                            if (np.isOccupied())// && np.givePlayer().Direction == 2)
                            {
                                return true;
                            }
                        }
                    }
                    if ((gHandler.giveGridPoint(i, me.getCurrentP().Y) is GridMap.Stone))//|| (gHandler.giveGridPoint(i, me.getCurrentP().Y) is GridMap.Coin) || (gHandler.giveGridPoint(i, me.getCurrentP().Y) is GridMap.LifePack))
                        break;
                    if (gHandler.giveGridPoint(i, me.getCurrentP().Y) is GridMap.Brick)
                    {
                        GridMap.Brick b = (GridMap.Brick)gHandler.giveGridPoint(i, me.getCurrentP().Y);
                        if (!b.isAccicible())
                            return true;
                    }
                }
            }
            if (me.Direction == 3)
            {
                for (int i = me.getCurrentP().X - 1; i >= 0; i--)
                {
                    if (me.getCurrentP().Y + ((int)i / 3) < Constant.MAP_SIZE)
                    {
                        if ((gHandler.giveGridPoint(i, me.getCurrentP().Y + ((int)i / 3)) is GridMap.NormalPoint))
                        {
                            GridMap.NormalPoint np = (GridMap.NormalPoint)(gHandler.giveGridPoint(i, me.getCurrentP().Y + ((int)i / 3)));
                            if (np.isOccupied())// && np.givePlayer().Direction == 0)
                            {
                                return true;
                            }
                        }
                    }
                    if (me.getCurrentP().Y - ((int)i / 3) >= 0)
                    {
                        if ((gHandler.giveGridPoint(i, me.getCurrentP().Y - ((int)i / 3)) is GridMap.NormalPoint))
                        {
                            GridMap.NormalPoint np = (GridMap.NormalPoint)(gHandler.giveGridPoint(i, me.getCurrentP().Y - ((int)i / 3)));
                            if (np.isOccupied())// && np.givePlayer().Direction == 2)
                            {
                                return true;
                            }
                        }
                    }
                    if ((gHandler.giveGridPoint(i, me.getCurrentP().Y) is GridMap.Stone))// || (gHandler.giveGridPoint(i, me.getCurrentP().Y) is GridMap.Coin) || (gHandler.giveGridPoint(i, me.getCurrentP().Y) is GridMap.LifePack))
                        break;
                    if (gHandler.giveGridPoint(i, me.getCurrentP().Y) is GridMap.Brick)
                    {
                        GridMap.Brick b = (GridMap.Brick)gHandler.giveGridPoint(i, me.getCurrentP().Y);
                        if (!b.isAccicible())
                            return true;
                    }
                }
            }
            return false;
        }

        private String giveRandomMove()
        {
            Random randomMove = new Random();
            int targetDir=0;
            do
            {
                nearestTarget = new GridMap.NormalDistanceGridPoint(new System.Drawing.Point(randomMove.Next(0, Constant.MAP_SIZE - 1), randomMove.Next(0, Constant.MAP_SIZE - 1)));
            } while (!(gHandler.giveGridPoint(nearestTarget.giveMapPoint().X, nearestTarget.giveMapPoint().Y) is GridMap.NormalPoint));

            while (nearestTarget.Prev != null && nearestTarget.Prev.giveMapPoint() != me.getCurrentP())
            {                
                nearestTarget = nearestTarget.Prev;
            }

            if (nearestTarget.giveMapPoint().X >= 0 && nearestTarget.giveMapPoint().Y >= 0 && nearestTarget.giveMapPoint().X < Constant.MAP_SIZE && nearestTarget.giveMapPoint().Y < Constant.MAP_SIZE)
            {
                if (nearestTarget.giveMapPoint().X > me.getCurrentP().X)
                    targetDir = 1;
                if (nearestTarget.giveMapPoint().X < me.getCurrentP().X)
                    targetDir = 3;
                if (nearestTarget.giveMapPoint().Y > me.getCurrentP().Y)
                    targetDir = 2;

                if (targetDir == 0 ){
                        return Constant.UP;
                }
                if (targetDir == 1){
                        return Constant.RIGHT;
                }
                if (targetDir == 2){
                        return Constant.DOWN;
                }
                if (targetDir == 3 ){
                        return Constant.LEFT;
                }
            }
            return Constant.SHOOT;
        }
    }
}
