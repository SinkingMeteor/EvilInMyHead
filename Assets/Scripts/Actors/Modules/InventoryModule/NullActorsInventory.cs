using System;
using Sheldier.Item;

namespace Sheldier.Actors.Inventory
{
    public class NullActorsInventory : IActorsInventory
    {
        public event Action<ItemDynamicConfigData> OnEquipItem;
        public bool IsItemTypeExists(string typeName) => false;
        public bool IsItemExists(string guid) => false;

        public InventoryOperationReport AddItem(ItemDynamicConfigData item)
        {
            return InventoryOperationReport.FailReport;
        }

        public InventoryOperationReport RemoveItem(string guid)
        {
            return InventoryOperationReport.FailReport;
        }

        public InventoryOperationReport RemoveItemAmount(string typeName, int amount = 1)
        {
            return InventoryOperationReport.FailReport;
        }
    }
}