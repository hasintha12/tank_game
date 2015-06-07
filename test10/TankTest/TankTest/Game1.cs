using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System.Threading;

namespace TankTest
{
    
    class Rocket {

        Point rocketCurrentPosition;
        int rocketDirection;

        public Rocket(int x,int y,int dir) {

            rocketCurrentPosition = new Point(x,y);
            rocketDirection = dir;
        } 

        public Point getRocketCurrentPosition() {
            return rocketCurrentPosition;
        }

        public int getRocketDirection() {
            return rocketDirection;
        }
        public void updatePoint(int x, int y)
        {
            rocketCurrentPosition = new Point(x, y);
        }
    }


    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        panzerController tankBrain;
        GraphicsDevice device;
        private Thread processThread;
        Rectangle[,] screenRectangle;
        Texture2D cellTexture;
        Texture2D brickTexture100;
        Texture2D brickTexture75;
        Texture2D brickTexture50;
        Texture2D brickTexture25;
        Texture2D waterTexture;
        Texture2D stoneTexture;
        Texture2D lifepackTexture;
        Texture2D coinTexture;
        Texture2D tankupTexture;
        Texture2D tankdownTexture;
        Texture2D tankleftTexture;
        Texture2D tankrightTexture;
        Texture2D rocketupTexture;
        Texture2D rocketleftTexture;
        Texture2D rocketdownTexture;
        Texture2D rocketrightTexture;

        GridHandler gH;
        PlayerHandler pH;

        Color playerColor;
        SpriteFont font1, font2,font3,font4,font5;

        List<Rocket> rock = new List<Rocket>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            screenRectangle = new Rectangle[20, 20];
            
        }

       
        protected override void Initialize()
        {
            
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "PANZER- The Ultimate warrior";          
            
            base.Initialize();            
        }
       
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;
            cellTexture = Content.Load<Texture2D>("cell");

            brickTexture100 = Content.Load<Texture2D>("brick_texture");
            brickTexture75 = Content.Load<Texture2D>("brick_texture_75");
            brickTexture50 = Content.Load<Texture2D>("brick_texture_50");
            brickTexture25 = Content.Load<Texture2D>("brick_texture_25");

            waterTexture = Content.Load<Texture2D>("water_texture");
            stoneTexture = Content.Load<Texture2D>("stone_texture");
            lifepackTexture = Content.Load<Texture2D>("lifepack");
            coinTexture = Content.Load<Texture2D>("coin");

            tankupTexture = Content.Load<Texture2D>("tank_up");
            tankdownTexture = Content.Load<Texture2D>("tank_down");
            tankleftTexture = Content.Load<Texture2D>("tank_left");
            tankrightTexture = Content.Load<Texture2D>("tank_right");

            rocketupTexture = Content.Load<Texture2D>("rocket_up");
            rocketleftTexture = Content.Load<Texture2D>("rocket_left");
            rocketdownTexture = Content.Load<Texture2D>("rocket_down");
            rocketrightTexture = Content.Load<Texture2D>("rocket_right");

            font1 = Content.Load<SpriteFont>("SpriteFont1");
            font2 = Content.Load<SpriteFont>("SpriteFont2");
            font3 = Content.Load<SpriteFont>("SpriteFont3");
            font4 = Content.Load<SpriteFont>("SpriteFont4");
            font5 = Content.Load<SpriteFont>("SpriteFont5");

            tankBrain = new panzerController();
            
            processThread = new Thread(new ThreadStart(tankBrain.process));
            processThread.Priority = ThreadPriority.Normal;
            tankBrain.startGame();
            tankBrain.waitGameStarted();          
            processThread.Start();

            gH = tankBrain.giveGridHandler();
            pH = gH.givePlayerHandler();
            
        }

        
      
       
        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

              this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f/3.0f);
            
            if (tankBrain.isFinished())
            {
                processThread.Abort();
                Window.Title = "Game Finished";
            }

            for (int i = 0; i < rock.Count;i++)
            {
                Rocket r = rock[i];

                if(r.getRocketDirection()==0){

                    int xx = r.getRocketCurrentPosition().X;
                    int yy = r.getRocketCurrentPosition().Y-1;

                    if (!(yy < 0 || (gH.giveGridPoint(xx, yy) is GridMap.Brick) || (gH.giveGridPoint(xx, yy) is GridMap.Stone)))
                    {
                        if (!(gH.giveGridPoint(xx, yy) is GridMap.Water))
                        {
                            GridMap.NormalPoint np = (GridMap.NormalPoint)gH.giveGridPoint(xx, yy);
                            if (!(np.isOccupied()))
                            {
                                r.updatePoint(xx, yy);
                            }
                            else
                            {
                                rock.Remove(r);
                            }
                        }
                        else
                        {
                            r.updatePoint(xx, yy);
                        }
                    }
                    else{
                        rock.Remove(r);
                    }
                }

                if (r.getRocketDirection() == 1)
                {

                    int xx = r.getRocketCurrentPosition().X+1;
                    int yy = r.getRocketCurrentPosition().Y;

                    if (!(xx >=Constant.MAP_SIZE || (gH.giveGridPoint(xx, yy) is GridMap.Brick) || (gH.giveGridPoint(xx, yy) is GridMap.Stone)))
                    {
                        if (!(gH.giveGridPoint(xx, yy) is GridMap.Water))
                        {
                            GridMap.NormalPoint np = (GridMap.NormalPoint)gH.giveGridPoint(xx, yy);
                            if (!(np.isOccupied()))
                            {
                                r.updatePoint(xx, yy);
                            }
                            else
                            {
                                rock.Remove(r);
                            }
                        }
                        else
                        {
                            r.updatePoint(xx, yy);
                        }
                    }
                    else
                    {
                        rock.Remove(r);
                    }
                }

                if (r.getRocketDirection() == 2)
                {

                    int xx = r.getRocketCurrentPosition().X;
                    int yy = r.getRocketCurrentPosition().Y+1 ;

                    if (!(yy >= Constant.MAP_SIZE || (gH.giveGridPoint(xx, yy) is GridMap.Brick) || (gH.giveGridPoint(xx, yy) is GridMap.Stone)))
                    {
                        if (!(gH.giveGridPoint(xx, yy) is GridMap.Water))
                        {
                            GridMap.NormalPoint np = (GridMap.NormalPoint)gH.giveGridPoint(xx, yy);
                            if (!(np.isOccupied()))
                            {
                                r.updatePoint(xx, yy);
                            }
                            else
                            {
                                rock.Remove(r);
                            }
                        }
                        else
                        {
                            r.updatePoint(xx, yy);
                        }
                    }
                    else
                    {
                        rock.Remove(r);
                    }
                }

                if (r.getRocketDirection() == 3)
                {

                    int xx = r.getRocketCurrentPosition().X-1;
                    int yy = r.getRocketCurrentPosition().Y ;

                    if (!(xx < 0 || (gH.giveGridPoint(xx, yy) is GridMap.Brick) || (gH.giveGridPoint(xx, yy) is GridMap.Stone)))
                    {
                        if (!(gH.giveGridPoint(xx, yy) is GridMap.Water))
                        {
                            GridMap.NormalPoint np = (GridMap.NormalPoint)gH.giveGridPoint(xx, yy);
                            if (!(np.isOccupied()))
                            {
                                r.updatePoint(xx, yy);
                            }
                            else
                            {
                                rock.Remove(r);
                            }
                        }
                        else
                        {
                            r.updatePoint(xx, yy);
                        }
                    }
                    else
                    {
                        rock.Remove(r);
                    }
                }
            }

            base.Update(gameTime);
        }


        
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            
            spriteBatch.Begin();
            DrawCells();
            DrawSpecificCells();
            DrawPlayers();
            DrawRocket();
            DrawText();
            spriteBatch.End();
            if (!tankBrain.isCommunicationAvailabel())
            {
                Window.Title = "Communication Error";
            }
            else
            {
                Window.Title = "Panzer-The Ultimate Warrior";
            }
            base.Draw(gameTime);
        }

        

        private void DrawCells()
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            for (int x = 0; x < Constant.MAP_SIZE; x++)
            {

                for (int y = 0; y < Constant.MAP_SIZE; y++)
                {

                    screenRectangle[x, y] = new Rectangle(0 + ((600 / Constant.MAP_SIZE) * x), 0 + ((600 / Constant.MAP_SIZE) * y), (600 / Constant.MAP_SIZE), (600 / Constant.MAP_SIZE));
                    spriteBatch.Draw(cellTexture, screenRectangle[x, y], Color.LightSlateGray);
                }
            }

        }

        private void DrawSpecificCells() {

            for (int x = 0; x < Constant.MAP_SIZE; x++)
            {

                for (int y = 0; y < Constant.MAP_SIZE; y++)
                {

                    if (gH.giveGridPoint(x,y) is GridMap.NormalPoint)
                    {
                        if (gH.giveGridPoint(x, y) is GridMap.Brick){

                            GridMap.Brick br=(GridMap.Brick)gH.giveGridPoint(x,y);

                            if(br.getDamageLevel()==0)
                                spriteBatch.Draw(brickTexture100, screenRectangle[x, y], Color.LightSlateGray);
                            else if (br.getDamageLevel() == 1)
                                spriteBatch.Draw(brickTexture75, screenRectangle[x, y], Color.LightSlateGray);
                            else if (br.getDamageLevel() == 2)
                                spriteBatch.Draw(brickTexture50, screenRectangle[x, y], Color.LightSlateGray);
                            else if (br.getDamageLevel() == 3)
                                spriteBatch.Draw(brickTexture25, screenRectangle[x, y], Color.LightSlateGray);
                            else if (br.getDamageLevel() == 4)
                                spriteBatch.Draw(cellTexture, screenRectangle[x, y], Color.LightSlateGray);
                        }
                            
                        else if (gH.giveGridPoint(x, y) is GridMap.Coin)
                            spriteBatch.Draw(coinTexture, screenRectangle[x, y], Color.LightSlateGray);
                            

                        else if (gH.giveGridPoint(x, y) is GridMap.LifePack)
                            spriteBatch.Draw(lifepackTexture, screenRectangle[x, y], Color.LightSlateGray); 

                    }

                    if (gH.giveGridPoint(x, y) is GridMap.Stone)
                        spriteBatch.Draw(stoneTexture, screenRectangle[x, y], Color.LightSlateGray);
                    

                    if (gH.giveGridPoint(x, y) is GridMap.Water)
                        spriteBatch.Draw(waterTexture, screenRectangle[x, y], Color.LightSlateGray);
                      

                }
            }
        }
        /*drawing player in grid*/
        private void DrawPlayers() {

            for (int i = 0; i< pH.givePlayersCount(); i++)
            {
                int x = pH.givePlayer(i).getCurrentP().X;
                int y = pH.givePlayer(i).getCurrentP().Y;

                screenRectangle[x, y] = new Rectangle(0 + ((600 / Constant.MAP_SIZE) * x), 0 + ((600 / Constant.MAP_SIZE) * y), (600 / Constant.MAP_SIZE), (600 / Constant.MAP_SIZE));

                if (pH.givePlayer(i).Health > 0)
                {

                    if(pH.giveMyPlayerIndex()==i)
                        playerColor = Color.BlueViolet;  
                    else
                        playerColor = Color.Red;


                    if (pH.givePlayer(i).Shot == true)
                    {
                        rock.Add(new Rocket(x, y, pH.givePlayer(i).Direction));
                    }

                    if (pH.givePlayer(i).Direction == 0)
                    {
                        spriteBatch.Draw(tankupTexture, screenRectangle[x, y], playerColor);

                    }

                    else if (pH.givePlayer(i).Direction == 1)
                    {
                        spriteBatch.Draw(tankrightTexture, screenRectangle[x, y], playerColor);
    
                    }

                    else if (pH.givePlayer(i).Direction == 2)
                    {
                        spriteBatch.Draw(tankdownTexture, screenRectangle[x, y], playerColor);
                    }

                    else if (pH.givePlayer(i).Direction == 3)
                    {
                        spriteBatch.Draw(tankleftTexture, screenRectangle[x, y], playerColor);
                    }

                    }
                
            }
        }
        /*draw rocket in grid*/
        public void DrawRocket() { 
        
            foreach (Rocket rockets in rock){
                
                if(rockets.getRocketDirection()==0){
                    spriteBatch.Draw(rocketupTexture, screenRectangle[rockets.getRocketCurrentPosition().X, rockets.getRocketCurrentPosition().Y], Color.Gold);
                }

                else if (rockets.getRocketDirection() == 1)
                {
                    spriteBatch.Draw(rocketrightTexture, screenRectangle[rockets.getRocketCurrentPosition().X, rockets.getRocketCurrentPosition().Y], Color.Gold);
                }

                else if (rockets.getRocketDirection() == 2)
                {
                    spriteBatch.Draw(rocketdownTexture, screenRectangle[rockets.getRocketCurrentPosition().X, rockets.getRocketCurrentPosition().Y], Color.Gold);
                }

                else if (rockets.getRocketDirection() == 3)
                {
                    spriteBatch.Draw(rocketleftTexture, screenRectangle[rockets.getRocketCurrentPosition().X, rockets.getRocketCurrentPosition().Y], Color.Gold);
                }
            }
        }

        private void DrawText()
        {
            spriteBatch.DrawString(font1, "Name", new Vector2(625, 100), Color.White);
            spriteBatch.DrawString(font1, "Score", new Vector2(700, 100), Color.White);
            spriteBatch.DrawString(font1, "Coins", new Vector2(775, 100), Color.White);
            spriteBatch.DrawString(font1, "Life", new Vector2(850, 100), Color.White);

            for (int i = 0; i < pH.givePlayersCount(); i++)
            {
                Color color;
                if (i == pH.giveMyPlayerIndex())
                {
                    color = Color.Blue;
                }
                else if(pH.givePlayer(i).Health==0){
                    color = Color.Red;
                }
                else
                {
                    color = Color.Green;
                }
                spriteBatch.DrawString(font2, pH.givePlayer(i).Name , new Vector2(625, 125+25*i), color);
                spriteBatch.DrawString(font2, pH.givePlayer(i).PointsEarned+"" , new Vector2(700, 125 + 25 * i), color);
                spriteBatch.DrawString(font2, "$" + pH.givePlayer(i).Coins , new Vector2(775, 125 + 25 * i), color);
                spriteBatch.DrawString(font2, pH.givePlayer(i).Health + "%", new Vector2(850, 125 + 25 * i), color);                
            }
            if (pH.givePlayer(pH.giveMyPlayerIndex()).Health == 0)
            {
                Window.Title = "Panzer lost!";
            }
            spriteBatch.DrawString(font3, "Panzer", new Vector2(665, 40), Color.Brown);           

            
        }

    }
 }

