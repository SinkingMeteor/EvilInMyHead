using System;
using Sheldier.Item;

namespace Sheldier.Actors.Inventory
{
    public class NullActorsInventory : IActorsInventory
    {
        #pragma warning disable
        public event Action<SimpleItem> OnItemUse;
        #pragma warning restore

        public bool IsItemExists(ItemConfig itemConfig) => true;

        public InventoryOperationReport AddItem(SimpleItem item)
        {
            return new InventoryOperationReport() {IsCompleted = true, Amount = item.ItemAmount.Amount};
        }

        public InventoryOperationReport RemoveItem(SimpleItem item)
        {
            return new InventoryOperationReport() {IsCompleted = true, Amount = item.ItemAmount.Amount};
        }

        public InventoryOperationReport RemoveItemAmount(ItemConfig item, int amount = 1)
        {
            return new InventoryOperationReport() {IsCompleted = true, Amount = amount};
        }

    }
}