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
        public static Texture2D cursor;
        public static Texture2D character;
        public static Texture2D shadow;
        public static Texture2D stoneBlock;
        public static Texture2D grassBlock;
        public static Texture2D dirtBlock;
        public static Texture2D tinBlock;
        public static Texture2D ironBlock;
        public static Texture2D titaniumBlock;

        public static void Load(ContentManager content)
        {
            cursor = content.Load<Texture2D>("Sprites/Cursor");
            character = content.Load<Texture2D>("Sprites/Character");
            shadow = content.Load<Texture2D>("Sprites/Blocks/Shadow");
            stoneBlock = content.Load<Texture2D>("Sprites/Blocks/StoneBlock");
            grassBlock = content.Load<Texture2D>("Sprites/Blocks/GrassBlock");
            dirtBlock = content.Load<Texture2D>("Sprites/Blocks/DirtBlock");
            tinBlock = content.Load<Texture2D>("Sprites/Blocks/TinBlock");
            ironBlock = content.Load<Texture2D>("Sprites/Blocks/IronBlock");
            titaniumBlock = content.Load<Texture2D>("Sprites/Blocks/TitaniumBlock");
        }
    }
}
