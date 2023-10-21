using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                Debug.WriteLine("stfu");
                return;
            }
            uiElements.Add(element);
        }
        public static void UpdateCanvas()
        {
            uiElements.ForEach(e => e.UpdateElement());
        }
        public static void Render(SpriteBatch spriteBatch)
        {
            uiElements.ForEach(e => e.Render(spriteBatch));
        }
    }
    public enum UIAlignment { TopLeft, MidLeft, BottomLeft, TopMid, Center, BottomMid, TopRight, MidRight, BottomRight }
}