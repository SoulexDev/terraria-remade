using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaRemade.Content.Engine
{
    public static class TileManager
    {
        public static int tileSize = 8;
        public static float scale = 3;
        public static List<TileMap> chunks = new List<TileMap>();
        public static int chunkAmount = 10;

        public static Tile GetTile(int x, int y)
        {
            TileMap map = GetLocalMap(x);

            if (map == null)
                return null;

            int localX = x - (int)map.transform.position.X;
            return TileDictionary.tileDictionary[map.map[localX, y]];
        }
        public static TileMap GetLocalMap(int x)
        {
            foreach (var tileMap in chunks)
            {
                float chunkSize = tileMap.map.GetLength(0) * tileSize * scale;

                float chunkPosition = tileMap.transform.position.X;
                if (x >= chunkPosition && x < chunkPosition + chunkSize)
                {
                    //Debug.WriteLine($"Mouse X - {x}, Chunk Position - {chunkPosition}, Chunk Name - {tileMap.name}");
                    return tileMap;
                }
            }
            return null;
        }
        public static TileMap GetTileMapFromTileSpace(int x, float worldOffset)
        {
            float worldCoordinate = x * tileSize * scale + worldOffset;
            return GetLocalMap((int)worldCoordinate);
        }
        public static int AdjustedTileCoord(int coord, int mapXLength)
        {
            coord -= mapXLength;
            return coord;
        }
        public static void Render(SpriteBatch spriteBatch)
        {
            chunks.ForEach(c => c.Render(spriteBatch));
        }
    }
}