using System.Collections.Generic;
using Sheldier.Item;
using UnityEngine;

namespace Sheldier.Actors.Inventory
{
    public class InventoryGroup
    {
        public IReadOnlyList<SimpleItem> Items => _collection;

        protected readonly IItemStorage _itemStorage;
        protected List<SimpleItem> _collection;

        public int Count => _collection.Count;

        public InventoryGroup(IItemStorage itemStorage)
        {
            _itemStorage = itemStorage;
            _collection = new List<SimpleItem>();
        }

        public bool IsExists(SimpleItem item) => _collection.Contains(item);

        public virtual InventoryOperationReport AddItem(SimpleItem item)
        {
            if (_itemStorage.GetFreeSlotsAmount() == 0)
                return InventoryOperationReport.FailReport;
            _collection.Add(item);
            return new InventoryOperationReport() {IsCompleted = true, Amount = 1};
        }

        public InventoryOperationReport RemoveSlot(SimpleItem item)
        {
            if (!IsExists(item))
                return InventoryOperationReport.FailReport;
            _collection.Remove(item);
            item.Drop();
            return new InventoryOperationReport() {IsCompleted = true, Amount = item.ItemAmount.Amount};
        }

        public virtual InventoryOperationReport RemoveAmount(int amountToRemove)
        {
            int remainsToRemove = amountToRemove;
            for (int i = 0; i < _collection.Count; i++)
            {
                var currentItemAmount = _collection[i].ItemAmount.Amount;
                int clampedAmount = Mathf.Min(currentItemAmount, remainsToRemove);
                _collection[i].ItemAmount.Remove(clampedAmount);
                remainsToRemove -= clampedAmount;
                
                if (_collection[i].ItemAmount.Amount == 0)
                {
                    _collection[i].Drop();
                    int lastIndex = _collection.Count - 1;
                    (_collection[i], _collection[lastIndex]) = (_collection[lastIndex], _collection[i]);
                    _collection.RemoveAt(lastIndex);
                    i -= 1;
                }
                
                if (remainsToRemove <= 0)
                    return new InventoryOperationReport(){IsCompleted = true, Amount = amountToRemove};
            }
            return new InventoryOperationReport(){IsCompleted = false, Amount = amountToRemove-remainsToRemove};
        }
    }
}