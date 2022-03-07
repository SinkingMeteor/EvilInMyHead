using Sheldier.Item;

namespace Sheldier.Actors.Inventory
{
    public class StackableInventoryGroup : InventoryGroup
    {
        public override void AddItem(SimpleItem item)
        {
            if(Count > 0)
                _collection[0].ItemAmount.Add(2);
            else
                _collection.Add(item); 
        }

        public override int RemoveAmount(int amountToRemove)
        {
            var currentItemAmount = _collection[0].ItemAmount.Amount;
            if (amountToRemove < currentItemAmount)
            {
                _collection[0].ItemAmount.Remove(amountToRemove);
                return amountToRemove;
            }
            _collection.RemoveAt(0);
            return currentItemAmount;
        }
    }
}