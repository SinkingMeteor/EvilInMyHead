using Sheldier.Item;
using UnityEngine;

namespace Sheldier.Actors.Inventory
{
    public class StackableInventoryGroup : InventoryGroup
    {
        public StackableInventoryGroup(IItemStorage itemStorage) : base(itemStorage)
        {
        }

        public override InventoryOperationReport AddItem(SimpleItem item)
        {
            int totalAmountToAdd = item.ItemAmount.Amount;
            int remainedAmountToAdd = totalAmountToAdd;
            foreach (var simpleItem in _collection)
            {
                int clampedAmount = Mathf.Min(remainedAmountToAdd, simpleItem.FreeAmount);
                simpleItem.ItemAmount.Add(clampedAmount);
                remainedAmountToAdd -= clampedAmount;

                if (remainedAmountToAdd <= 0)
                    return new InventoryOperationReport() {IsCompleted = true, Amount = totalAmountToAdd};
            }

            int freeSlots = _itemStorage.GetFreeSlotsAmount();

            if (freeSlots == 0)
                return new InventoryOperationReport()
                    {IsCompleted = false, Amount = totalAmountToAdd - remainedAmountToAdd};

            _collection.Add(item);
            item.ItemAmount.Set(remainedAmountToAdd);
            return new InventoryOperationReport() {IsCompleted = true, Amount = totalAmountToAdd};
        }
        
        
        public override InventoryOperationReport RemoveAmount(int amountToRemove)
        {
            _collection.Sort(((item1, item2) => item1.ItemAmount.Amount < item2.ItemAmount.Amount ? -1 : 1));
            return base.RemoveAmount(amountToRemove);
        }
    }
}