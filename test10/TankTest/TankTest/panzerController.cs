using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TankTest
{
    class panzerController
    {        
        
        private NetworkConnection conn;
        private Thread reciveThread;
        private GridHandler gHandler;
        private ErrorHandler eHandler;
        private AILogicHandler aiHandler;
        private bool finished;
        public panzerController()
        {
            conn = new NetworkConnection();            
            reciveThread = new Thread(new ThreadStart(conn.ReceiveData));
            reciveThread.Priority = ThreadPriority.Highest;
            gHandler = new GridHandler(conn);
            eHandler = new ErrorHandler(conn);
            aiHandler = new AILogicHandler(gHandler, eHandler);
            finished = false;
        }
        public GridHandler giveGridHandler()
        {
            return gHandler;
        }
        public void startGame()
        {
            reciveThread.Start();
            do
            {
                conn.SendData(new DataObject("JOIN#", Constant.SERVER_IP, Constant.SERVER_PORT));
                Thread.Sleep(4057);
            } while (!conn.connectionAvailability());   //loop until server starts listning to clients
            Console.WriteLine("joined");
            while (!conn.GameIAccepted())
            {
                Thread.Sleep(200);
                if (eHandler.giveJoinError() != null)
                {
                    return; ///a code to gui to display join error
                }
            }  
        }

        public void waitGameStarted()
        {

            while (!conn.GameSAccepted())
            {
                Thread.Sleep(200);
            }
            gHandler.loadGrid();
            aiHandler.loadPlayers();
            
        }
        public bool isGameStarted()
        {
            return conn.GameSAccepted();
        }
        public void process()
        {
            String command = aiHandler.giveNextCommand(); ;
            while (!eHandler.isGameFinished())
            {
                if (!eHandler.isMyPlayerDead())
                {
                    if (command != Constant.STOP)
                    {
                        if (!conn.isNewGMsg())
                        {
                            conn.SendData(new DataObject(command, Constant.SERVER_IP, Constant.SERVER_PORT));
                            Thread.Sleep(1000);
                        }
                    }
                    if (eHandler.giveMovingShootingError() == Constant.S2C_TOOEARLY)
                    {
                        Random sleepTime = new Random();
                        Thread.Sleep(sleepTime.Next(1, 25));
                        Console.WriteLine("Wait random time to resend=====================================================");
                        continue;
                    }
                }
                if (eHandler.isGameFinished())
                {
                    finished = true;
                    break;///display in gui
                    //throw new GameFinishedException();
                }
                command = aiHandler.giveNextCommand();
                //Thread.Sleep(1000);
            }

        }
        public bool isFinished()
        {
            return finished;
        }
        public Boolean isCommunicationAvailabel()
        {
            return conn.connectionAvailability();
        }
    }
}
