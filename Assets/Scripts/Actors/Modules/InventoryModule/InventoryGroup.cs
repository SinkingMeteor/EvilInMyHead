using System.Collections.Generic;
using System.Linq;
using Sheldier.Item;
using UnityEngine;

namespace Sheldier.Actors.Inventory
{
    public class InventoryGroup
    {
        public IReadOnlyDictionary<string, ItemDynamicConfigData> Items => _collection;

        protected readonly IItemStorage _itemStorage;
        protected Dictionary<string, ItemDynamicConfigData>  _collection;

        public int Count => _collection.Count;

        public InventoryGroup(IItemStorage itemStorage)
        {
            _itemStorage = itemStorage;
            _collection = new Dictionary<string, ItemDynamicConfigData>();
        }

        public bool IsExists(string guid) => _collection.ContainsKey(guid);

        public virtual InventoryOperationReport AddItem(ItemDynamicConfigData dynamicConfigData)
        {
            if (_itemStorage.GetFreeSlotsAmount() == 0)
                return InventoryOperationReport.FailReport;
            _collection.Add(dynamicConfigData.ID, dynamicConfigData);
            return new InventoryOperationReport() {IsCompleted = true, Amount = 1};
        }

        public InventoryOperationReport RemoveSlot(string guid)
        {
            if (!IsExists(guid))
                return InventoryOperationReport.FailReport;
            int itemAmount = _collection[guid].Amount;
            _collection.Remove(guid);
            return new InventoryOperationReport() {IsCompleted = true, Amount = itemAmount};
        }

        public InventoryOperationReport RemoveAmount(int amountToRemove)
        {
            int remainsToRemove = amountToRemove;
            List<string> guidsToRemove = new List<string>();
            foreach (var dynamicConfigData in _collection)
            {
                var currentItemAmount = dynamicConfigData.Value.Amount;
                int clampedAmount = Mathf.Min(currentItemAmount, remainsToRemove);
                dynamicConfigData.Value.Amount -= clampedAmount;
                remainsToRemove -= clampedAmount;
                
                if (dynamicConfigData.Value.Amount == 0)
                    guidsToRemove.Add(dynamicConfigData.Key);
                
                if (remainsToRemove <= 0)
                    break;
            }

            for (int i = 0; i < guidsToRemove.Count; i++)
                _collection.Remove(guidsToRemove[i]);
            
            if(remainsToRemove <= 0)
                return new InventoryOperationReport(){IsCompleted = true, Amount = amountToRemove};

            return new InventoryOperationReport(){IsCompleted = false, Amount = amountToRemove-remainsToRemove};
        }
    }
}