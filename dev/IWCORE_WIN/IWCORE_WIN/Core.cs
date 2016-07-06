/*
  Copyright olonaut Studios
  Do not distribute! 
*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IWCORE_WIN
{
    public class Core : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Map testmap;
        Player player;
        Texture2D roomTex;

        /* Constants for DEMO */
        Color ROOMCOL = Color.DarkGray;

        public Core()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "IRON WINTER DEVBUILD";
            testmap = new Map();
            player = new Player();
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
        }
        protected override void UnloadContent()
        {
            
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            spriteBatch.Begin();
            spriteBatch.Draw(roomTex,testmap.demoRoom.pos);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
