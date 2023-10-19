using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaRemade.Content.Engine
{
    public static class Canvas
    {
        private static List<UIElement> uiElements = new List<UIElement>();

        public static void Instantiate(UIElement element)
        {
            if (element == null)
            {
                return;
            }
            uiElements.Add(element);
        }
        public static void Render(SpriteBatch spriteBatch)
        {
            uiElements.ForEach(e => e.renderer.Render(spriteBatch, e.transform, 
                positionOffset: Camera.Instance.transform.position - GameRoot.ScreenSize / 2));
        }
    }
    public enum UIAlignment { TopLeft, MidLeft, BottomLeft, TopMid, Center, BottomMid, TopRight, MidRight, BottomRight }
}