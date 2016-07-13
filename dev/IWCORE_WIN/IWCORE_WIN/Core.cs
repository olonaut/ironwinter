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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Map testmap;
        Player player;
        Texture2D roomTex;
        Crosshair crsshr;
        Texture2D bulletTex;
        Bullet[] bullets;
        SpriteFont debugFont;
        
        /* Constants for DEMO */
        Color ROOMCOL = Color.DarkGray;
        public static float PLAYERSPEED = 5;

        public Core()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "IRON WINTER DEVBUILD";
            this.IsMouseVisible = false;
            testmap = new Map();
            player = new Player(new Vector2(testmap.demoRoom.pos.X + (testmap.demoRoom.size.X / 2) + 32, testmap.demoRoom.pos.Y + (testmap.demoRoom.size.Y / 2) + 32));
            crsshr = new Crosshair();

            bullets = new Bullet[(int)Math.Pow(2,8)];
        }
        protected override void Initialize()
        {
            base.Initialize();
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
            bulletTex = Content.Load<Texture2D>("sprites\\bullet");

            /* fonts */
            debugFont = Content.Load<SpriteFont>("font\\debug");
        }
        protected override void UnloadContent()
        {
            roomTex.Dispose();
            player.texture.Dispose();
            crsshr.texture.Dispose();
            bulletTex.Dispose();
        }

        private short bulletcount = 0;
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
                    bullets[bulletcount] = new Bullet(player.pos, player.facing);
                    bulletcount++;
                    if (bulletcount >= bullets.Length)
                    {
                        bulletcount = 0;
                    }
                }
            }
            /* Bullet calc */
            foreach (Bullet b in bullets)
            {
                if (b.active)
                {
                    
                }
            }

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            spriteBatch.Begin();
            spriteBatch.Draw(roomTex,testmap.demoRoom.pos);
            spriteBatch.Draw(player.texture,player.pos,null,Color.White,player.facing, player.origin,1.0f,SpriteEffects.None,0f);
            spriteBatch.Draw(crsshr.texture,crsshr.pos);
            spriteBatch.DrawString(debugFont,bulletcount.ToString(),new Vector2(0, graphics.GraphicsDevice.Viewport.Height - debugFont.MeasureString(bulletcount.ToString()).Y),Color.Blue);
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
