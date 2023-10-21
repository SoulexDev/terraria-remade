using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrariaRemade.Content.Engine;

namespace TerrariaRemade.Content.Scripts
{
    public static class ItemManager
    {
        public static InventoryItem grassBlock;
        public static InventoryItem dirtBlock;
        public static InventoryItem stoneBlock;

        public static void CreateItems()
        {
            grassBlock = new InventoryItem("Grass Block", TextureLoader.grassBlock);
            dirtBlock = new InventoryItem("Dirt Block", TextureLoader.dirtBlock);
            stoneBlock = new InventoryItem("Stone Block", TextureLoader.stoneBlock);
        }
    }
}