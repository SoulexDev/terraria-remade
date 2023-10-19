using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaRemade.Content.Engine
{
    public static class TileDictionary
    {
        public static Dictionary<int, Tile> tileDictionary = new Dictionary<int, Tile>()
        {
            { 0, null },
            { 1, new Tile(TextureLoader.grassBlock) },
            { 2, new Tile(TextureLoader.dirtBlock) },
            { 3, new Tile(TextureLoader.stoneBlock) },
            { 4, new Tile(TextureLoader.tinBlock) },
            { 5, new Tile(TextureLoader.ironBlock) },
            { 6, new Tile(TextureLoader.titaniumBlock) }
        };
    }
}
