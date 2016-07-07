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
        private float playerRotAngle = 0;

        /* Constants for DEMO */
        Color ROOMCOL = Color.DarkGray;

        public Core()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "IRON WINTER DEVBUILD";
            this.IsMouseVisible = false;
            testmap = new Map();
            player = new Player(new Vector2(testmap.demoRoom.pos.X + (testmap.demoRoom.size.X / 2) + 32 , testmap.demoRoom.pos.Y + (testmap.demoRoom.size.Y / 2) + 32));
            crsshr = new Crosshair();
        }
        protected override void Initialize()
        {
            base.Initialize();

        }
        protected override void LoadContent()
        {
            /* This is for Demo */
            Color[] roomColor = new Color[(int)testmap.demoRoom.size.X * (int)testmap.demoRoom.size.Y];
            for (int i = 0; i < roomColor.Length; i++) roomColor[i] = ROOMCOL;
            roomTex = new Texture2D(graphics.GraphicsDevice, (int)testmap.demoRoom.size.X, (int)testmap.demoRoom.size.Y);
            roomTex.SetData(roomColor);

            spriteBatch = new SpriteBatch(GraphicsDevice);
            player.texture = Content.Load<Texture2D>("sprites\\plr");
            /* set player texture origin in order to rot8 */
            player.setOrigin(new Vector2(player.texture.Width/2 , player.texture.Height/2));

            crsshr.texture = Content.Load<Texture2D>("sprites\\crosshairs");

        }
        protected override void UnloadContent()
        {
            roomTex.Dispose();
            player.texture.Dispose();
            crsshr.texture.Dispose();
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
            MouseState mouseState = Mouse.GetState();

            var direction = (new Vector2(mouseState.X, mouseState.Y) - (player.getPos()));
            playerRotAngle = (float)Math.Atan2(direction.Y, direction.X) + MathHelper.PiOver2;

            // playerRotAngle = GamePad.GetState(PlayerIndex.One).Triggers.Right * 100;

            /* Conversion, to be put in new funciton */
            // playerRotAngle = ((playerRotAngle - 0) / (100 - 0)) * ((MathHelper.Pi * 2) - 0) + 0;
            

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            spriteBatch.Begin();
            spriteBatch.Draw(roomTex,testmap.demoRoom.pos);
            spriteBatch.Draw(player.texture,player.getPos(),null,Color.White,playerRotAngle, player.getOrigin(),1.0f,SpriteEffects.None,0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
