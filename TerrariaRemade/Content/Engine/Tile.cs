using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaRemade.Content.Engine
{
    public class Tile
    {
        public Texture2D sprite;

        public Tile(Texture2D sprite)
        {
            this.sprite = sprite;
        }
    }
}
