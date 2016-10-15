/*
  Copyright olonaut Studios
  Do not distribute! 
*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace IWCORE_WIN
{
    public class Core : Game
    {
        // Modifiers and Settings
        RasterizerState RS = new RasterizerState { MultiSampleAntiAlias = true };

        //Global Objects
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Map testmap;
        Player player;
        Texture2D roomTex;
        Crosshair crsshr;
        Shot[] shots;
        Texture2D bulletLine;
        SpriteFont debugFont;
        
        // Constants for Demo
        Color ROOMCOL = Color.DarkGray;
        public static float PLAYERSPEED = 5;

        string DEBUG = "";

        public Core()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "IRON WINTER DEVBUILD";
            this.IsMouseVisible = false;
            testmap = new Map();
            player = new Player(new Vector2(testmap.demoRoom.pos.X + (testmap.demoRoom.size.X / 2) + 32, testmap.demoRoom.pos.Y + (testmap.demoRoom.size.Y / 2) + 32));
            crsshr = new Crosshair();
        }

        protected override void Initialize()
        {
            base.Initialize();
            graphics.PreferMultiSampling = true;
        }

        protected override void LoadContent()
        {
            /* This is for Demo */
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Color[] roomColor = new Color[(int)testmap.demoRoom.size.X * (int)testmap.demoRoom.size.Y];
            for (int i = 0; i < roomColor.Length; i++) roomColor[i] = ROOMCOL;
            roomTex = new Texture2D(graphics.GraphicsDevice, (int)testmap.demoRoom.size.X, (int)testmap.demoRoom.size.Y);
            roomTex.SetData(roomColor);

            player.texture = Content.Load<Texture2D>("sprites\\plr");
            /* set player texture origin in order to rot8 */
            player.origin = new Vector2(player.texture.Width/2 , player.texture.Height/2);

            /* misc textures */
            crsshr.texture = Content.Load<Texture2D>("sprites\\crosshairs");

            /* fonts */
            debugFont = Content.Load<SpriteFont>("font\\debug");

            /* Bullet Line Texture */
            bulletLine = new Texture2D(GraphicsDevice, 1, 1);
            bulletLine.SetData<Color>(
                new Color[] { Color.White });
        }
        protected override void UnloadContent()
        {
            roomTex.Dispose();
            player.texture.Dispose();
            crsshr.texture.Dispose();
        }
        
        private double elapsedMS = 0;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
            MouseState mouseState = Mouse.GetState();
            KeyboardState kbState = Keyboard.GetState();
            GamePadState padState = GamePad.GetState(PlayerIndex.One);
            Vector2 playerMove = new Vector2(0,0);

            /* Crosshairs */
            /* Player Movement */
            /* Could be done better */
            crsshr.pos = new Vector2(Mouse.GetState().X - (crsshr.texture.Width / 2), Mouse.GetState().Y - (crsshr.texture.Height / 2));
            var direction = (new Vector2(crsshr.pos.X + (crsshr.texture.Width / 2) , crsshr.pos.Y + (crsshr.texture.Height / 2)) - (player.pos));
            player.facing = (float)Math.Atan2(direction.Y, direction.X) + MathHelper.PiOver2;

            Vector2 dirVect = new Vector2((float)Math.Cos(player.facing), (float)Math.Sin(player.facing));
            if (dirVect.X < -1 || dirVect.X > 1) dirVect.X = 0;
            if (dirVect.Y < -1 || dirVect.Y > 1) dirVect.Y = 0;

            /* Collision */
            if (kbState.IsKeyDown(Keys.W) && !(testmap.demoRoom.pos.Y > player.pos.Y - (player.texture.Height / 2) - (PLAYERSPEED)))
                playerMove.Y -= PLAYERSPEED;
            if (kbState.IsKeyDown(Keys.S) && !(testmap.demoRoom.pos.Y + testmap.demoRoom.size.Y < player.pos.Y + (player.texture.Height / 2) + (PLAYERSPEED)))
                playerMove.Y += PLAYERSPEED;
            if (kbState.IsKeyDown(Keys.A) && !(testmap.demoRoom.pos.X > player.pos.X - (player.texture.Width / 2) - (PLAYERSPEED)))
                playerMove.X -= PLAYERSPEED;
            if (kbState.IsKeyDown(Keys.D) && !(testmap.demoRoom.pos.X + testmap.demoRoom.size.X < player.pos.X + (player.texture.Width / 2) + (PLAYERSPEED)))
                playerMove.X += PLAYERSPEED;
            /* TODO DISALLOW STRAVING */
            player.pos += playerMove; /* Apply previously calculated player movement */
            
            /* Shooting */
            if(mouseState.LeftButton == ButtonState.Pressed)
            {
                elapsedMS += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (elapsedMS >= Player.SHOOTINGSPEED)
                {
                    elapsedMS = 0;
                    //TODO implement shooting
                }
            }
            DEBUG = dirVect.ToString() + "\n" + player.pos.ToString() + "\n" + player.texture.ToString();

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
            spriteBatch.Begin(SpriteSortMode.FrontToBack,null,null,null,RS); //Enable anti aliasing
            spriteBatch.Draw(roomTex,testmap.demoRoom.pos); // Draw Room
            spriteBatch.Draw(player.texture,player.pos,null,Color.White,player.facing, player.origin,1.0f,SpriteEffects.None,0f);
            spriteBatch.DrawString(debugFont,DEBUG,new Vector2(0, 0),Color.Blue);
            Line.Draw(spriteBatch,bulletLine, player.pos /* + new Vector2( player.texture.Width/2 , player.texture.Height/2)*/, crsshr.pos + new Vector2(crsshr.texture.Width/2 , crsshr.texture.Height / 2), Color.Red); //Draw Line [For Debugging Only]
            spriteBatch.Draw(crsshr.texture,crsshr.pos); // Draw Crosshair last, for layering
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public double radToPercent(double rad)
        {
            double per;
            per = ((rad - 0) / (MathHelper.PiOver2 - 0)) * (100 - 0) + 0;
            return per;
        }

        public double percentToRad(double per)
        {
            double rad;
            rad = ((per - 0) / (100 - 0)) * (MathHelper.PiOver2 - 0) + 0;
            return rad;
        }

    }
}
