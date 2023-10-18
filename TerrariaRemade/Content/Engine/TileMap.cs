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
    public class TileMap
    {
        public string name;
        public Transform transform = new Transform();
        public int[,] map = new int[64, 256];
        public Color[,] lightMap = new Color[64, 256];
        public float scale = 5;
        public int tileSize = 8;

        private bool updateLighting = false;

        public void Generate()
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    FillTile(x, y, 2);
                }
            }
        }
        public void FillTile(int x, int y, int tileID)
        {
            map[x, y] = tileID;
            updateLighting = true;
        }

        public void Render(SpriteBatch spriteBatch)
        {
            if (updateLighting)
            {
                CalculateLighting(1, 16);
                updateLighting = false;
            }
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    float worldX = transform.position.X + x;
                    float worldY = transform.position.Y + y;

                    if (!Camera.Instance.IsInFrustum(new Vector2(worldX, worldY) * tileSize * scale))
                        continue;

                    if (lightMap[x, y].ToVector3().Length() < 0.3f && TileExists(x, y))
                    { 
                        spriteBatch.Draw(TextureLoader.shadow, new Vector2(worldX, worldY) * tileSize * scale,
                        null, Color.White, 0, Vector2.Zero, scale * 8, 0, 0);
                        continue;
                    }

                    Tile tile = TileDictionary.tileDictionary[map[x, y]];

                    if (tile == null)
                        continue;
                    Texture2D sprite = tile.sprite;

                    spriteBatch.Draw(sprite, new Vector2(worldX, worldY) * tileSize * scale,
                        null, lightMap[x, y], 0, Vector2.Zero, scale, 0, 0);
                }
            }
        }
        private void CalculateLighting(int shadowPenalty = 1, int maxLightDepth = 8)
        {
            for (int x = 0; x < lightMap.GetLength(0); x++)
            {
                int lightDepth = 0;
                bool hitTile = false;
                bool exitTile = false;

                for (int y = 0; y <= x; y++)
                {
                    int xCoord = x - y;
                    int yCoord = y;

                    if(TileExists(xCoord, yCoord))
                    {
                        hitTile = true;
                        float lightFactor = 1 - (float)(lightDepth + (exitTile ? shadowPenalty : 0)) / maxLightDepth;
                        
                        lightMap[xCoord, yCoord] = new Color(lightFactor, lightFactor, lightFactor);

                        lightDepth++;
                    }
                    else if(hitTile)
                        exitTile = true;
                }
            }

            int yLength = lightMap.GetLength(1);
            int xLength = lightMap.GetLength(0);

            for (int y = 0; y < yLength; y++)
            {
                int lightDepth = 0;
                bool hitTile = false;
                bool exitTile = false;

                for (int x = xLength; x >= 0; x--)
                {
                    int xCoord = x;
                    int yCoord = y + xLength - x;

                    if (TileExists(xCoord, yCoord))
                    {
                        hitTile = true;
                        float lightFactor = 1 - (float)(lightDepth + (exitTile ? shadowPenalty : 0)) / maxLightDepth;

                        lightMap[xCoord, yCoord] = new Color(lightFactor, lightFactor, lightFactor);

                        lightDepth++;
                    }
                    else if (hitTile)
                        exitTile = true;
                }
            }
            for (int x = 0; x < lightMap.GetLength(0); x++)
            {
                for (int y = 0; y < lightMap.GetLength(1); y++)
                {
                    lightMap[x, y] = lightMap[x, y].Add(GetBlurColor(x, y, 4));
                }
            }
        }
        public Color GetBlurColor(int xCoord, int yCoord, int blurRadius)
        {
            float valueSum = 0;
            int maxValue = 0;
            for (int x = -blurRadius; x < blurRadius; x++)
            {
                for (int y = -blurRadius; y < blurRadius; y++)
                {
                    maxValue++;
                    if (TileExists(xCoord + x, yCoord + y))
                        valueSum += lightMap[xCoord + x, yCoord + y].R / 255;
                    else
                        valueSum++;
                }
            }
            float blur = valueSum / maxValue;
            return new Color(blur, blur, blur);
        }
        private float GetTileFactor(int x, int y, int radius)
        {
            int tileCount = 0;
            int maxTileCount = 0;
            for (int z = -radius; z < radius; z++)
            {
                for (int w = -radius; w < radius; w++)
                {
                    maxTileCount++;
                    if (TileExists(x + z, y + w))
                        tileCount++;
                }
            }
            return 1 - tileCount / (maxTileCount * 1.2f);
        }
        //private static float GetTileCircle(int x, int y, int radius)
        //{
        //    int tileCount = 0;
        //    int maxPossibleTileCount = 0;
        //    for (int row = y - radius; row < y + radius; row++)
        //    {
        //        int rowDif = row - y;
        //        int columnRange = (int)MathF.Sqrt(radius * radius - rowDif * rowDif);

        //        for (int column = x - columnRange; column < x + columnRange; column++)
        //        {
        //            maxPossibleTileCount++;

        //            if(TileExists(column, row))
        //            {
        //                tileCount++;
        //            }
        //        }
        //    }
        //    return 1 - tileCount / (maxPossibleTileCount * 1.2f);
        //}

        public bool TileExists(int x, int y)
        {
            return TileInBounds(x, y) ? map[x, y] != 0 : false;
        }
        public bool TileInBounds(int x, int y)
        {
            return !(x < 0 || y < 0 || x > map.GetLength(0) - 1 || y > map.GetLength(1) - 1);
        }
        public Tile GetTile(int x, int y)
        {
            return TileDictionary.tileDictionary[map[x, y]];
        }
        public Vector2 ScreenToTilePosition(Vector2 position)
        {
            position /= (tileSize * scale);

            int x = (int)position.X;
            int y = (int)position.Y;

            return new Vector2(x, y) - transform.position;
        }
    }
}