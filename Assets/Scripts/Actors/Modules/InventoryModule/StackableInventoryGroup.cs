using Sheldier.Item;
using UnityEngine;

namespace Sheldier.Actors.Inventory
{
    public class StackableInventoryGroup : InventoryGroup
    {
        public StackableInventoryGroup(IItemStorage itemStorage) : base(itemStorage)
        {
        }

        public override InventoryOperationReport AddItem(ItemDynamicConfigData item)
        {
            int totalAmountToAdd = item.Amount;
            int remainedAmountToAdd = totalAmountToAdd;
            foreach (var simpleItem in _collection)
            {
                int clampedAmount = Mathf.Min(remainedAmountToAdd, simpleItem.Value.MaxStack - simpleItem.Value.Amount);
                simpleItem.Value.Amount += clampedAmount;
                remainedAmountToAdd -= clampedAmount;

                if (remainedAmountToAdd <= 0)
                    return new InventoryOperationReport() {IsCompleted = true, Amount = totalAmountToAdd};
            }

            int freeSlots = _itemStorage.GetFreeSlotsAmount();

            if (freeSlots == 0)
                return new InventoryOperationReport()
                    {IsCompleted = false, Amount = totalAmountToAdd - remainedAmountToAdd};

            _collection.Add(item.Guid, item);
            item.Amount = remainedAmountToAdd;
            return new InventoryOperationReport() {IsCompleted = true, Amount = totalAmountToAdd};
        }
    }
}