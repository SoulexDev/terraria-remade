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
        public static Texture2D character;
        public static Texture2D shadow;

        //Tiles
        public static Texture2D stoneBlock;
        public static Texture2D grassBlock;
        public static Texture2D dirtBlock;
        public static Texture2D tinBlock;
        public static Texture2D ironBlock;
        public static Texture2D titaniumBlock;

        //UIElements
        public static Texture2D cursor;
        public static Texture2D itemSlot;

        public static void Load(ContentManager content)
        { 
            character = content.Load<Texture2D>("Sprites/Character");
            shadow = content.Load<Texture2D>("Sprites/Blocks/Shadow");

            //Tiles
            stoneBlock = content.Load<Texture2D>("Sprites/Blocks/StoneBlock");
            grassBlock = content.Load<Texture2D>("Sprites/Blocks/GrassBlock");
            dirtBlock = content.Load<Texture2D>("Sprites/Blocks/DirtBlock");
            tinBlock = content.Load<Texture2D>("Sprites/Blocks/TinBlock");
            ironBlock = content.Load<Texture2D>("Sprites/Blocks/IronBlock");
            titaniumBlock = content.Load<Texture2D>("Sprites/Blocks/TitaniumBlock");

            //UIElements
            cursor = content.Load<Texture2D>("Sprites/UI/Cursor");
            itemSlot = content.Load<Texture2D>("Sprites/UI/ItemSlot");
        }
    }
}
