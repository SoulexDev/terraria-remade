using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaRemade.Content.Engine
{
    public class Transform
    {
        public Vector2 position;
        public float rotation;
        public Vector2 scale;

        public Vector2 origin;

        public Vector2 forward 
        { 
            get 
            {
                return new Vector2(MathF.Sin(rotation), MathF.Cos(rotation));
            }
            set
            {
                rotation = MathF.Atan2(MathF.Cos(value.X), MathF.Sin(value.Y));
            }
        }
        public Vector2 up
        {
            get
            {
                return new Vector2(MathF.Cos(rotation), MathF.Sin(rotation));
            }
            set
            {
                rotation = MathF.Atan2(MathF.Sin(value.Y), MathF.Cos(value.X));
            }
        }
    }
    public class Renderer
    {
        public Color color = Color.White;
        public float layerDepth;
        public Texture2D sprite;

        public void Render(SpriteBatch spriteBatch, Transform transform = null, Vector2 position = default, Vector2 positionOffset = default)
        {
            if (sprite == null)
                return;

            if(transform != null)
            {
                spriteBatch.Draw(sprite, transform.position + positionOffset, null, color, transform.rotation, transform.origin, transform.scale, 0, layerDepth);
            }
        }
        public Vector2 size
        {
            get
            {
                return sprite == null ? Vector2.Zero : new Vector2(sprite.Width, sprite.Height);
            }
        }
    }
}
