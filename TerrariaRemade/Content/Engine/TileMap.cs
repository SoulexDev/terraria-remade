using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaRemade.Content.Engine
{
    static class TileMap
    {
        public static int[,] map = new int[64,64];
        public static Color[,] lightMap = new Color[64,64];
        public static float scale = 5;
        public static int tileSize = 8;

        private static bool updateLighting = false;

        public static void FillTile(int x, int y, int tileID)
        {
            map[x, y] = tileID;
            updateLighting = true;
        }

        public static void Render(SpriteBatch spriteBatch)
        {
            if (updateLighting)
            {
                CalculateLighting();
                updateLighting = false;
            }
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (lightMap[x, y].ToVector3().Length() < 0.4f && TileExists(x, y))
                    { 
                        spriteBatch.Draw(TextureLoader.shadow, new Vector2(x, y) * tileSize * scale,
                        null, Color.White, 0, Vector2.Zero, scale * 8, 0, 0);
                        continue;
                    }     

                    Tile tile = TileDictionary.tileDictionary[map[x, y]];

                    if (tile == null)
                        continue;
                    Texture2D sprite = tile.sprite;

                    spriteBatch.Draw(sprite, new Vector2(x, y) * tileSize * scale,
                        null, lightMap[x, y], 0, Vector2.Zero, scale, 0, 0);
                }
            }
        }
        private static void CalculateLighting()
        {
            for (int x = 0; x < lightMap.GetLength(0); x++)
            {
                for (int y = 0; y < lightMap.GetLength(1); y++)
                {
                    lightMap[x, y] = GetLighting(x, y);
                }
            }
        }

        public static Color GetLighting(int x, int y)
        {
            float factor = GetTileFactor(x, y, 8);
            return new Color(factor, factor, factor);
            int spread = 8;

            float upLeft = 8;
            float up = 8;
            float upRight = 8;
            float midLeft = 8;
            float midRight = 8;
            float downLeft = 8;
            float down = 8;
            float downRight = 8;

            CalculateLightFactor(x, y, ref upLeft, new Vector2(-1, 1), spread);
            CalculateLightFactor(x, y, ref up, new Vector2(0, 1), spread);
            CalculateLightFactor(x, y, ref upRight, new Vector2(1, 1), spread);
            CalculateLightFactor(x, y, ref midLeft, new Vector2(-1, 0), spread);
            CalculateLightFactor(x, y, ref midRight, new Vector2(1, 0), spread);
            CalculateLightFactor(x, y, ref downLeft, new Vector2(-1, -1), spread);
            CalculateLightFactor(x, y, ref down, new Vector2(0, -1), spread);
            CalculateLightFactor(x, y, ref downRight, new Vector2(1, -1), spread);

            float lightingFactor = (upLeft + up + upRight + midLeft + midRight + downLeft + down + downRight) / (spread * 8f);
            return new Color(lightingFactor, lightingFactor, lightingFactor);
        }
        private static void CalculateLightFactor(int x, int y, ref float factor, Vector2 direction, int spread = 4)
        {
            for (int i = 1; i < spread; i++)
            {
                if (TileExists(x + ((int)direction.X * i), y + ((int)direction.Y) * i))
                {
                    factor--;
                }
                else
                {
                    factor++;
                    break;
                }
            }
        }
        private static float GetTileFactor(int x, int y, int radius)
        {
            int tileCount = 0;
            for (int z = -radius; z < radius; z++)
            {
                for (int w = -radius; w < radius; w++)
                {
                    if (!TileExists(x + z, y + w))
                        tileCount++;
                }
            }
            return (float)tileCount/(radius * radius);
        }

        public static bool TileExists(int x, int y)
        {
            return TileInBounds(x, y) ? map[x, y] != 0 : false;
        }
        public static bool TileInBounds(int x, int y)
        {
            return !(x < 0 || y < 0 || x > map.GetLength(0) - 1 || y > map.GetLength(1) - 1);
        }
        public static Tile GetTile(int x, int y)
        {
            return TileDictionary.tileDictionary[map[x, y]];
        }
        public static Vector2 ScreenToTilePosition(Vector2 position)
        {
            position /= (tileSize * scale);

            int x = (int)position.X;
            int y = (int)position.Y;

            return new Vector2(x, y);
        }
    }
}