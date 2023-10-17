using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaRemade.Content.Engine
{
    public static class Cursor
    {
        public static Texture2D cursorSprite;
        public static Transform transform = new Transform();
        private static TileCollider tileCollider = new TileCollider();

        public static void Init()
        {
            tileCollider.transform = transform;
            
            cursorSprite = TextureLoader.cursor;
            tileCollider.rectangle = new Rectangle((int)transform.position.X, (int)transform.position.Y, cursorSprite.Width, cursorSprite.Height);

            transform.scale = Vector2.One * 2;
        }
        public static void Update()
        {
            Vector2 prevPos = transform.position;
            transform.position = Input.MousePosition;
            tileCollider.CheckCollisions(prevPos);
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(cursorSprite, transform.position, null, Color.White, 0, transform.origin, transform.scale, 0, 1);
        }
    }
}