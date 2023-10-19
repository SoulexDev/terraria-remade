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
    public class UIGridLayout : Entity
    {
        public UIGridLayout()
        {
            uiElements.CollectionChanged += UpdateLayout;
        }
        public Transform transform;
        public ObservableCollection<UIElement> uiElements = new ObservableCollection<UIElement>();
        public int elementsSizeX;
        //public UIAlignment alignment;

        private float currentXSize;
        private void UpdateLayout(object sender, NotifyCollectionChangedEventArgs e)
        {
            currentXSize = 0;
            for (int x = 0, y = 0; x < uiElements.Count; x++)
            {
                if (x + 1 >= elementsSizeX)
                    y++;
                UIElement element = uiElements[x];

                currentXSize += element.renderer.size.X;

                element.transform.position = new Vector2(currentXSize, y * element.renderer.size.Y) + transform.position;
            }
        }

        public override void Awake()
        {
            
        }

        public override void Start()
        {
            
        }

        public override void Update()
        {
            
        }
    }
}