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
        public static float scale = 1;
        public static List<TileMap> chunks = new List<TileMap>();
        private static int chunkAmount = 10;

        public static void GenerateChunks()
        {
            for (int i = 0; i < chunkAmount; i++)
            {
                TileMap map = new TileMap();
                map.transform.position.X = i * map.map.GetLength(0);

                map.tileSize = tileSize;
                map.scale = scale;

                map.name = i.ToString();
                map.Generate();
                chunks.Add(map);
            }
        }
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
                float xPosition = tileMap.transform.position.X * tileSize;
                if (x >= xPosition && x < xPosition + tileMap.map.GetLength(0) * tileSize * scale)
                {
                    return tileMap;
                }
            }
            return null;
        }
        public static void Render(SpriteBatch spriteBatch)
        {
            chunks.ForEach(c => c.Render(spriteBatch));
        }
    }
}