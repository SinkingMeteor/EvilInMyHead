using System;
using Sheldier.Item;

namespace Sheldier.Actors.Inventory
{
    public interface IActorsInventory
    {
        public event Action<ItemDynamicConfigData> OnEquipItem;
        bool IsItemTypeExists(string typeName);
        bool IsItemExists(string guid);
        InventoryOperationReport AddItem(ItemDynamicConfigData item);
        InventoryOperationReport RemoveItem(string guid);
        InventoryOperationReport RemoveItemAmount(string typeName, int amount = 1);
    }
}