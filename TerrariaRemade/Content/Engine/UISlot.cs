using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaRemade.Content.Engine
{
    public class UISlot : UIElement
    {
        public UIElement slotItemElement = new UIElement();

        public UISlot()
        {
            slotItemElement.transform.scale *= 3;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            base.Render(spriteBatch);
            slotItemElement.Render(spriteBatch);
        }
        public override void UpdateElement()
        {
            base.UpdateElement();
            slotItemElement.transform.position = transform.position + renderer.size * transform.scale / 2
                - slotItemElement.renderer.size * slotItemElement.transform.scale / 2;
        }
    }
}