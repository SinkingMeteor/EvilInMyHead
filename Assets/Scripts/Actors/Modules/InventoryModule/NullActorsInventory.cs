using System;
using Sheldier.Item;

namespace Sheldier.Actors.Inventory
{
    public class NullActorsInventory : IActorsInventory
    {
        public event Action<SimpleItem> OnItemUse;

        private const int LARGE_NUMBER = 9999;

        public bool IsItemExists(ItemConfig itemConfig) => true;

        public bool AddItem(SimpleItem item) => true;

        public void RemoveItem(SimpleItem item)
        {
        }

        public int RemoveItem(ItemConfig item, int amount = 1)
        {
            return LARGE_NUMBER;
        }

    }
}