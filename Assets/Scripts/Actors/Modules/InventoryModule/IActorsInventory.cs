using System;
using Sheldier.Item;

namespace Sheldier.Actors.Inventory
{
    public interface IActorsInventory
    {

        public event Action<SimpleItem> OnItemUse;
        bool IsItemExists(ItemConfig itemConfig);
        InventoryOperationReport AddItem(SimpleItem item);
        InventoryOperationReport RemoveItem(SimpleItem item);
        InventoryOperationReport RemoveItemAmount(ItemConfig item, int amount = 1);
    }
}