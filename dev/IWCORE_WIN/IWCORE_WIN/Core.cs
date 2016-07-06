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

        public Core()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "IRON WINTER DEVBUILD";
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
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
            spriteBatch.Begin();
            /* heer goez de grafix */
            spriteBatch.End();

            GraphicsDevice.Clear(Color.Gray);
            base.Draw(gameTime);
        }
    }
}
