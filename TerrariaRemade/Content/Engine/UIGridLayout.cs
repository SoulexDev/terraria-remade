using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaRemade.Content.Engine
{
    public class UIGridLayout
    {
        public UIGridLayout()
        {
            uiElements.CollectionChanged += UpdateLayout;
        }
        public Transform transform = new Transform();
        public ObservableCollection<UIElement> uiElements = new ObservableCollection<UIElement>();
        public int elementsSizeX;
        //public UIAlignment alignment;

        private float currentXSize;
        private void UpdateLayout(object sender, NotifyCollectionChangedEventArgs e)
        {
            currentXSize = 0;
            for (int x = 0, y = 0, xPos = 0; x < uiElements.Count; x++)
            {
                if (xPos + 1 > elementsSizeX)
                {
                    y++;
                    xPos = 0;
                }
                    
                UIElement element = uiElements[x];

                Vector2 position = new Vector2(xPos, y) * element.renderer.size * element.transform.scale;
                element.transform.position = position + transform.position;
                xPos++;
            }
        }
    }
}