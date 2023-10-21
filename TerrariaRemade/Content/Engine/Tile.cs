using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrariaRemade.Content.Scripts;

namespace TerrariaRemade.Content.Engine
{
    public class Tile
    {
        public Texture2D sprite;
        public InventoryItem item;

        public Tile(Texture2D sprite)
        {
            this.sprite = sprite;
        }
    }
}
