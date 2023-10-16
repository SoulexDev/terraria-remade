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

        public static void Init()
        {
            cursorSprite = TextureLoader.cursor;
            transform.scale = Vector2.One * 2;
        }
        public static void Update()
        {
            transform.position = Input.MousePosition;
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(cursorSprite, transform.position, null, Color.White, 0, transform.origin, transform.scale, 0, 1);
        }
    }
}