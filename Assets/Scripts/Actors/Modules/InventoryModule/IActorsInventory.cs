using System;
using Sheldier.Item;

namespace Sheldier.Actors.Inventory
{
    public interface IActorsInventory
    {

        public event Action<SimpleItem> OnItemUse;
        bool IsItemExists(ItemConfig itemConfig);
        bool AddItem(SimpleItem item);
        void RemoveItem(SimpleItem item);
        int RemoveItem(ItemConfig item, int amount = 1);
    }
}