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
        public static int[,] map = new int[128,128];
        public static int[,] lightMap = new int[128,128];
        public static float scale = 5;
        public static int tileSize = 8;

        public static void FillTile(int x, int y, int tileID)
        {
            map[x, y] = tileID;
        }

        public static void Render(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    Tile tile = TileDictionary.tileDictionary[map[x, y]];

                    if (tile == null)
                        continue;
                    Texture2D sprite = tile.sprite;

                    spriteBatch.Draw(sprite, new Vector2(x, y) * tileSize * scale,
                        null, Color.White, 0, Vector2.Zero, scale, 0, 0);
                }
            }
        }

        public static Color GetLighting(int x, int y)
        {
            float upLeft = TileExists(x - 1, y + 1) ? 0 : 1;
            float up = TileExists(x, y + 1) ? 0 : 1;
            float upRight = TileExists(x + 1, y + 1) ? 0 : 1;
            float midLeft = TileExists(x - 1, y) ? 0 : 1;
            float mid = TileExists(x, y) ? 0 : 1;
            float midRight = TileExists(x + 1, y + 1) ? 0 : 1;
            float downLeft = TileExists(x - 1, y - 1) ? 0 : 1;
            float down = TileExists(x, y - 1) ? 0 : 1;
            float downRight = TileExists(x + 1, y - 1) ? 0 : 1;

            float light = (upLeft + up + upRight + midLeft + mid + midRight + downLeft + down + downRight) / 9;

            return new Color(light, light, light);

            float upFactor = 8;
            float rightFactor = 8;
            float downFactor = 8;
            float leftFactor = 8;

            for (int u = 0; u < 8; u++)
            {
                if (TileExists(x, y + u))
                {
                    upFactor--;
                }
                else
                {
                    upFactor++;
                }
            }
            for (int r = 0; r < 8; r++)
            {
                if (TileExists(x + r, y))
                {
                    rightFactor--;
                }
                else
                {
                    rightFactor++;
                }
            }
            for (int d = 0; d < 8; d++)
            {
                if (TileExists(x, y - d))
                {
                    downFactor--;
                }
                else
                {
                    downFactor++;
                }
            }
            for (int l = 0; l < 8; l++)
            {
                if (TileExists(x - l, y))
                {
                    leftFactor--;
                }
                else
                {
                    leftFactor++;
                }
            }

            upFactor = Math.Clamp(upFactor + 1, -1, 8)/8;
            rightFactor = Math.Clamp(rightFactor + 1, -1, 8)/8;
            downFactor = Math.Clamp(downFactor + 1, -1, 8)/8;
            leftFactor = Math.Clamp(leftFactor + 1, -1, 8)/8;

            float lightingFactor = (upFactor + rightFactor + downFactor + leftFactor)/4;
            return new Color(lightingFactor, lightingFactor, lightingFactor);
        }

        public static bool TileExists(int x, int y)
        {
            return x < 0 || y < 0 ? false : map[x, y] != 0;
        }
    }
}