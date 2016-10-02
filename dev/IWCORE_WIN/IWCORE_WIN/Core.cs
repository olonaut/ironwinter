﻿/*
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
        Bullet[] bullets;
        Texture2D bulletLine;
        SpriteFont debugFont;
        
        /* Constants for DEMO */
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

            Vector2 dirVect = new Vector2((float)Math.Cos(player.facing), (float)Math.Sin(player.facing));
            if (dirVect.X < -1 || dirVect.X > 1) dirVect.X = 0;
            if (dirVect.Y < -1 || dirVect.Y > 1) dirVect.Y = 0;
            DEBUG = dirVect.ToString();

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
                    bulletcount++;
                    if (bulletcount >= bullets.Length)
                    {
                        bulletcount = 0;
                    }
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
            spriteBatch.DrawString(debugFont,DEBUG,new Vector2(0, graphics.GraphicsDevice.Viewport.Height - debugFont.MeasureString(DEBUG).Y),Color.Blue);
            //DrawLine(spriteBatch,bulletLine,new Vector2(200, 200),new Vector2(600,600)); //Draw Line [For Debugging Only]
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

        void DrawLine(SpriteBatch sb, Texture2D _t, Vector2 start, Vector2 end)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);


            sb.Draw(_t,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    2), //width of line, change this to make thicker line
                null,
                Color.DarkOrange, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);

        }

        bool IsIntersecting(Point a, Point b, Point c, Point d)
        {
            float denominator = ((b.X - a.X) * (d.Y - c.Y)) - ((b.Y - a.Y) * (d.X - c.X));
            float numerator1 = ((a.Y - c.Y) * (d.X - c.X)) - ((a.X - c.X) * (d.Y - c.Y));
            float numerator2 = ((a.Y - c.Y) * (b.X - a.X)) - ((a.X - c.X) * (b.Y - a.Y));

            // Detect coincident lines (has a problem, read below)
            if (denominator == 0) return numerator1 == 0 && numerator2 == 0;

            float r = numerator1 / denominator;
            float s = numerator2 / denominator;

            return (r >= 0 && r <= 1) && (s >= 0 && s <= 1);
        }

    }
}
