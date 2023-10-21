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
        private InventoryManager inventoryManager = new InventoryManager();

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

            ItemManager.CreateItems();

            EntityManager.Instantiate(Camera.Instance);
            EntityManager.Instantiate(new BlockGrabber());
            EntityManager.Instantiate(new CameraController());
            Cursor.Init();

            UIGridLayout gridLayout = new UIGridLayout();
            gridLayout.elementsSizeX = 3;

            for (int i = 0; i < 9; i++)
            {
                UISlot itemSlot = new UISlot();
                itemSlot.renderer.sprite = TextureLoader.itemSlot;
                itemSlot.transform.scale *= 3;

                Canvas.Instantiate(itemSlot);
                gridLayout.uiElements.Add(itemSlot);

                InventorySlot slot = new InventorySlot(itemSlot);

                inventoryManager.slots.Add(slot);
            }

            inventoryManager.AddItem(ItemManager.grassBlock);
            inventoryManager.AddItem(ItemManager.stoneBlock);
            inventoryManager.AddItem(ItemManager.dirtBlock);

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
            Canvas.UpdateCanvas();
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