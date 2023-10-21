using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaRemade.Content.Scripts
{
    public class InventoryManager
    {
        public List<InventorySlot> slots = new List<InventorySlot>();

        public bool AddItem(InventoryItem item)
        {
            if (!slots.Exists(s => s.item == item || s.item == null))
                return false;

            InventorySlot slot = slots.First(s => s.item == item || s.item == null);
            if(slot.item == null)
                slot.item = item;
            else
                slot.itemAmount++;

            return true;
        }
        public bool RemoveItem(InventoryItem item)
        {
            if (!slots.Exists(s => s.item == item))
                return false;

            InventorySlot slot = slots.Last(s => s.item == item);
            slot.itemAmount--;

            return true;
        }
    }
}