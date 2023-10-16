using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Reflection.Metadata;
using TerrariaRemade.Content.Engine;
using TerrariaRemade.Content.Scripts;

namespace TerrariaRemade
{
    public class GameRoot : Game
    {
        public static GameRoot Instance { get; private set; }
        public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }

        private Camera cam = new Camera();

        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        public GameRoot()
        {
            Instance = this;

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();

            base.Initialize();

            EntityManager.Instantiate(cam);
            TileMap.scale = 3;

            for (int x = 0; x < TileMap.map.GetLength(0); x++)
            {
                for (int y = 0; y < TileMap.map.GetLength(1); y++)
                {
                    if(y <= 64 && x <= 64)
                    {
                        TileMap.FillTile(x, y, 1);
                    }
                }
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            TextureLoader.Load(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Time.deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Time.time = (float)gameTime.TotalGameTime.TotalSeconds;

            EntityManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);

            _spriteBatch.Begin(SpriteSortMode.Texture, BlendState.Opaque, SamplerState.PointClamp, transformMatrix: cam.Transform);

            EntityManager.Draw(_spriteBatch);
            TileMap.Render(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}