using System.Collections.Generic;
using Sheldier.Item;

namespace Sheldier.Actors.Inventory
{
    public class Inventory : IActorsInventory
    {
        public IReadOnlyDictionary<ItemConfig, InventoryGroup> ItemsCollection => _itemsCollection;
        private Dictionary<ItemConfig, InventoryGroup> _itemsCollection;

        public void Initialize()
        {
            _itemsCollection = new Dictionary<ItemConfig, InventoryGroup>();
        }

        public bool IsItemExists(ItemConfig itemConfig)
        {
            return  _itemsCollection.ContainsKey(itemConfig);
        }

        public bool AddItem(SimpleItem item)
        {
            if(!IsItemExists(item.ItemConfig))
                _itemsCollection.Add(item.ItemConfig, item.IsStackable ? new StackableInventoryGroup() : new InventoryGroup());
            _itemsCollection[item.ItemConfig].AddItem(item);
            return true;
        }

        public void RemoveItem(SimpleItem item)
        {
            if (!IsItemExists(item.ItemConfig))
                return;
            _itemsCollection[item.ItemConfig].RemoveItem(item);
            if (_itemsCollection[item.ItemConfig].Count == 0)
                _itemsCollection.Remove(item.ItemConfig);
        }

        public int RemoveItem(ItemConfig item, int amount = 1, int index = 0)
        {
            if (!IsItemExists(item))
                return 0;
            int returnedAmount = _itemsCollection[item].RemoveAmount(amount, index);
            if (_itemsCollection[item].Count == 0)
                _itemsCollection.Remove(item);
            return returnedAmount;
        }
        
    }
    public class InventoryGroup
    {
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

        public virtual int RemoveAmount(int amountToRemove, int index = 0)
        {
            if (_collection.Count < index + 1)
                return 0;
            
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

    public class StackableInventoryGroup : InventoryGroup
    {
        public StackableInventoryGroup() : base()
        {
        }


        public override void AddItem(SimpleItem item)
        {
            if(Count > 0)
                _collection[0].ItemAmount.Add(2);
            else
                _collection.Add(item); 
        }

        public override int RemoveAmount(int amountToRemove, int index = 0)
        {
            if (_collection.Count < index + 1)
                return 0;
                
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
