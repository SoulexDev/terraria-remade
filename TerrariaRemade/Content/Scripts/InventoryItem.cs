using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaRemade.Content.Scripts
{
    public class InventoryItem
    {
        public string name;
        public Texture2D icon;

        public InventoryItem(string name, Texture2D icon)
        {
            this.name = name;
            this.icon = icon;
        }
    }
}