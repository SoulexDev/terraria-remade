using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaRemade.Content.Engine
{
    public class UIElement
    {
        public Transform transform = new Transform();
        public Renderer renderer = new Renderer();

        public virtual void Render(SpriteBatch spriteBatch)
        {
            renderer.Render(spriteBatch, transform, 
                positionOffset: Camera.Instance.transform.position - GameRoot.ScreenSize / 2);
        }
        public virtual void UpdateElement()
        {

        }
    }
}