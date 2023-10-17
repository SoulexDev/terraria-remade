using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaRemade.Content.Engine
{
    public class BoxCollider
    {
        public Rectangle bounds;

        public void Init(int width, int height)
        {
            bounds.Width = width;
            bounds.Height = height;
        }
        public void UpdateBox(Transform transform)
        {
            bounds.Location = new Point();
        }
    }
}
