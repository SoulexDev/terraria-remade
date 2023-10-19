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

        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        private UIGridLayout hotBar = new UIGridLayout();

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

            EntityManager.Instantiate(Camera.Instance);
            EntityManager.Instantiate(new BlockGrabber());
            EntityManager.Instantiate(new CameraController());
            Cursor.Init();

            for (int i = 0; i < 9; i++)
            {
                UIElement itemSlot = new UIElement();
                itemSlot.renderer.sprite = TextureLoader.itemSlot;
                itemSlot.transform.scale *= 24;

                Canvas.Instantiate(itemSlot);
            }
            //TileMap.scale = 3;

            //for (int x = 0; x < TileMap.map.GetLength(0); x++)
            //{
            //    for (int y = 0; y < TileMap.map.GetLength(1); y++)
            //    {
            //        TileMap.FillTile(x, y, 2);
            //    }
            //}

            WorldGenerator.GenerateWorld();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            TextureLoader.Load(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            Time.deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Time.time = (float)gameTime.TotalGameTime.TotalSeconds;

            Input.Update();
            EntityManager.Update();
            Cursor.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, transformMatrix: Camera.Instance.Transform);

            TileManager.Render(_spriteBatch);
            EntityManager.Draw(_spriteBatch);
            Canvas.Render(_spriteBatch);
            Cursor.Draw(_spriteBatch);
            //_spriteBatch.Draw(TextureLoader.cursor, Input.MousePosition, null, Color.White, 0, Vector2.Zero, 5, 0, 1);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}