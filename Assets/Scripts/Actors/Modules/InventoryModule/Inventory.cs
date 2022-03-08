using System;
using System.Collections.Generic;
using Sheldier.Item;

namespace Sheldier.Actors.Inventory
{
    public class Inventory : IActorsInventory, IItemStorage
    {
        public event Action<SimpleItem> OnItemUse;
        public IReadOnlyDictionary<ItemConfig, InventoryGroup> ItemsCollection => _itemsCollection;
        private Dictionary<ItemConfig, InventoryGroup> _itemsCollection;
        private int _capacity = 12;
    
        public void Initialize()
        {
            _itemsCollection = new Dictionary<ItemConfig, InventoryGroup>();
        }
        public int GetFreeSlotsAmount()
        {
            int slotsCount = 0;
            foreach (var inventoryGroup in _itemsCollection)
                slotsCount += inventoryGroup.Value.Count;
            return _capacity - slotsCount;
        }
        public bool IsItemExists(ItemConfig itemConfig)
        {
            return  _itemsCollection.ContainsKey(itemConfig);
        }

        public InventoryOperationReport AddItem(SimpleItem item)
        {
            if (!IsItemExists(item.ItemConfig) && GetFreeSlotsAmount() == 0)
                return InventoryOperationReport.FailReport;
            if(!IsItemExists(item.ItemConfig))
                _itemsCollection.Add(item.ItemConfig, item.IsStackable ? new StackableInventoryGroup(this) : new InventoryGroup(this));
            return _itemsCollection[item.ItemConfig].AddItem(item);
            
        }

        public InventoryOperationReport RemoveItem(SimpleItem item)
        {
            if (!IsItemExists(item.ItemConfig))
                return InventoryOperationReport.FailReport;
            InventoryOperationReport report = _itemsCollection[item.ItemConfig].RemoveSlot(item);
            if (_itemsCollection[item.ItemConfig].Count == 0)
                _itemsCollection.Remove(item.ItemConfig);
            return report;
        }

        public void UseItem(SimpleItem item)
        {
            OnItemUse?.Invoke(item);
        }
        public InventoryOperationReport RemoveItemAmount(ItemConfig item, int amount = 1)
        {
            if (!IsItemExists(item))
                return InventoryOperationReport.FailReport;
            InventoryOperationReport report = _itemsCollection[item].RemoveAmount(amount);
            if (_itemsCollection[item].Count == 0)
                _itemsCollection.Remove(item);
            return report;
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
