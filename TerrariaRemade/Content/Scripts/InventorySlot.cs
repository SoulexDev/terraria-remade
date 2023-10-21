using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrariaRemade.Content.Engine;

namespace TerrariaRemade.Content.Scripts
{
    public class InventorySlot
    {
        private InventoryItem _item;
        public InventoryItem item
        {
            get 
            { 
                return _item; 
            }
            set
            {
                _item = value;
                if(_item == null)
                {
                    itemImage.renderer.sprite = null;
                    itemAmount = 0;
                    amountText = "";
                }
                else
                {
                    itemImage.renderer.sprite = _item.icon;
                    amountText = itemAmount.ToString();
                }
            }
        }
        private int _itemAmount;
        public int itemAmount
        {
            get
            {
                return _itemAmount;
            }
            set
            {
                _itemAmount = value;
                if (_itemAmount <= 0)
                    item = null;
            }
        }
        public InventorySlot(UISlot slot)
        {
            itemImage = slot.slotItemElement;
        }
        private UIElement itemImage;
        public string amountText;
    }
}