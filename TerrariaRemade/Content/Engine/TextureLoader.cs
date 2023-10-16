using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaRemade.Content.Engine
{
    static class TextureLoader
    {
        public static Texture2D stoneBlock;

        public static void Load(ContentManager content)
        {
            stoneBlock = content.Load<Texture2D>("Sprites/Blocks/StoneBlock");
        }
    }
}
