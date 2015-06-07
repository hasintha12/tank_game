using System;

namespace TankTest
{
    class PlayerHandler
    {
        private Player[] playerArray;
        private String myPlayer;
        private int myPlayerIndex;
        private NetworkConnection conn;
        private int myPrevHealth = 100;
        private bool targeted = false;
        public PlayerHandler(NetworkConnection con)
        {
            this.conn = con;
        }
        public void loadMyPlayerID()
        {
            String s = conn.giveIMsg();
            String s2 = s.Split(':')[1];
            myPlayer = s2;
        }
        public void loadPlayers()
        {
            loadMyPlayerID();
            String[] smsgArray = conn.giveSMsg().Split(':');  //smsg gives the list of connected players
            playerArray = new Player[smsgArray.Length - 1];
            for (int i = 1; i < smsgArray.Length; i++)
            {
                String pname = smsgArray[i].Split(';')[0];
                if (myPlayer == pname)
                {
                    myPlayerIndex = i - 1;
                }
                String scoord = smsgArray[i].Split(';')[1];
                String sdir = smsgArray[i].Split(';')[2];
                playerArray[i - 1] = new Player(pname, int.Parse(scoord.Split(',')[0]), int.Parse(scoord.Split(',')[1]), int.Parse(sdir.Substring(0, 1)));
            }
        }
        public void updatePlayerData()  //update the player locations from the gmsg data
        {
            String gmsg = conn.giveLastGmsg();
            if (gmsg != null)
                for (int i = 0; i < playerArray.Length; i++)
                {
                    String playeri = gmsg.Split(':')[i + 1];
                    int x, y, dir, shot, health, coin, points;
                    x = int.Parse(playeri.Split(';')[1].Split(',')[0]);
                    y = int.Parse(playeri.Split(';')[1].Split(',')[1]);
                    dir = int.Parse(playeri.Split(';')[2]);
                    shot = int.Parse(playeri.Split(';')[3]);
                    health = int.Parse(playeri.Split(';')[4]);
                    coin = int.Parse(playeri.Split(';')[5]);
                    points = int.Parse(playeri.Split(';')[6]);

                    playerArray[i].setCurrentP(new System.Drawing.Point(x, y));
                    playerArray[i].Direction = dir;
                    if (shot == 1)
                    {
                        playerArray[i].Shot = true;
                    }
                    else
                    {
                        playerArray[i].Shot = false;
                    }
                    playerArray[i].Health = health;
                    playerArray[i].Coins = coin;
                    playerArray[i].PointsEarned = points;
                }
            if (myPrevHealth != playerArray[myPlayerIndex].Health)
            {
                targeted = true;
                myPrevHealth = playerArray[myPlayerIndex].Health;
            }
            else
            {
                targeted = false;
            }
        }
        public int givePlayersCount()
        {
            return playerArray.Length;
        }
        public Player givePlayer(int i)
        {
            return playerArray[i];
        }
        public void setPlayer(int i, Player p)
        {
            playerArray[i] = p;
        }
        public int giveMyPlayerIndex()  //to get the index of my player form the array
        { 
            return myPlayerIndex;
        }
        public Boolean playerOnPoint(System.Drawing.Point p)
        {
            for (int i = 0; i < playerArray.Length; i++)
            {
                if (playerArray[i].getCurrentP() == p && playerArray[i].Health != 0)
                    return true;
            }
            return false;
        }
        public int playerDirOnPoint(System.Drawing.Point p)    //when we give a point this returns the direction of the player. this is to get the dicision whether shoot or not
        {
            for (int i = 0; i < playerArray.Length; i++)
            {
                if (playerArray[i].getCurrentP() == p && playerArray[i].Health != 0)
                    return playerArray[i].Direction;
            }
            return 5;
        }
        public bool isTargeted()   //whetether my player is targeted for shoot 
        {
            return targeted;
        }
    }
}
