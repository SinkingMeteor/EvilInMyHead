using System.Collections.Generic;
using Sheldier.Item;

namespace Sheldier.Actors.Inventory
{
    public class InventoryGroup
    {
        public IReadOnlyList<SimpleItem> Items => _collection;

        protected List<SimpleItem> _collection;

        public int Count => _collection.Count;

        public InventoryGroup()
        {
            _collection = new List<SimpleItem>();
        }

        public bool IsExists(SimpleItem item) => _collection.Contains(item);

        public virtual void AddItem(SimpleItem item)
        {
            _collection.Add(item);
        }

        public void RemoveItem(SimpleItem item)
        {
            if (!IsExists(item))
                return;
            _collection.Remove(item);
            item.Drop();
        }

        public virtual int RemoveAmount(int amountToRemove)
        {

            var currentItemAmount = _collection[0].ItemAmount.Amount;
            if (amountToRemove < currentItemAmount)
            {
                _collection[0].ItemAmount.Remove(amountToRemove);
                return amountToRemove;
            }
            _collection[0].Drop();
            _collection.RemoveAt(0);
            return currentItemAmount;
        }
    }
}