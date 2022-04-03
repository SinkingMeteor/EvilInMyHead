using System;
using System.Collections.Generic;
using Sheldier.Item;

namespace Sheldier.Actors.Inventory
{
    public class Inventory : IActorsInventory, IItemStorage
    {
        public event Action<ItemDynamicConfigData> OnItemUse;
        public IReadOnlyDictionary<string, InventoryGroup> ItemsCollection => _itemsCollection;
        private Dictionary<string, InventoryGroup> _itemsCollection;
        private int _capacity = 12;
    
        public void Initialize()
        {
            _itemsCollection = new Dictionary<string, InventoryGroup>();
        }
        public int GetFreeSlotsAmount()
        {
            int slotsCount = 0;
            foreach (var inventoryGroup in _itemsCollection)
                slotsCount += inventoryGroup.Value.Count;
            return _capacity - slotsCount;
        }

        public void UseItem(string guid)
        {
            if(TryGetItem(guid, out ItemDynamicConfigData dynamicConfigData))
                OnItemUse?.Invoke(dynamicConfigData);
        }

        public bool IsItemTypeExists(string typeName) => _itemsCollection.ContainsKey(typeName);

        public bool IsItemExists(string guid)
        {
            foreach (var itemGroup in _itemsCollection)
            {
                if (itemGroup.Value.Items.ContainsKey(guid))
                    return true;
            }

            return false;
        }

        public bool TryGetItem(string guid, out ItemDynamicConfigData dynamicConfigData)
        {
            foreach (var itemGroup in _itemsCollection)
            {
                if (itemGroup.Value.IsExists(guid))
                {
                    dynamicConfigData = itemGroup.Value.Items[guid];
                    return true;
                }
            }

            dynamicConfigData = null;
            return false;
        }
        public InventoryOperationReport AddItem(ItemDynamicConfigData item)
        {
            if (!IsItemTypeExists(item.ItemName) && GetFreeSlotsAmount() == 0)
                return InventoryOperationReport.FailReport;
            if(!IsItemTypeExists(item.ItemName))
                _itemsCollection.Add(item.ItemName, item.IsStackable ? new StackableInventoryGroup(this) : new InventoryGroup(this));
            return _itemsCollection[item.ItemName].AddItem(item);
        }

        public InventoryOperationReport RemoveItem(string guid)
        {
            foreach (var inventoryGroup in _itemsCollection)
            {
                if (inventoryGroup.Value.IsExists(guid))
                   return inventoryGroup.Value.RemoveSlot(guid);
            }
            return InventoryOperationReport.FailReport;
        }

        public InventoryOperationReport RemoveItemAmount(string typeName, int amount = 1)
        {
            if(!IsItemTypeExists(typeName))
                return InventoryOperationReport.FailReport;
            return _itemsCollection[typeName].RemoveAmount(amount);
        }
        
    }

    public struct InventoryOperationReport
    {
        public static readonly InventoryOperationReport FailReport = new InventoryOperationReport()
            {IsCompleted = false, Amount = 0}; 
        
        public bool IsCompleted;
        public int Amount;
    }

    public interface IItemStorage
    {
        public int GetFreeSlotsAmount();
    }
}
