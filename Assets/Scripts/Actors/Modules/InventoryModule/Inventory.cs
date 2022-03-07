using System;
using System.Collections.Generic;
using Sheldier.Item;

namespace Sheldier.Actors.Inventory
{
    public class Inventory : IActorsInventory
    {
        public event Action<SimpleItem> OnItemUse;
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

        public void UseItem(SimpleItem item)
        {
            OnItemUse?.Invoke(item);
        }
        public int RemoveItem(ItemConfig item, int amount = 1)
        {
            if (!IsItemExists(item))
                return 0;
            int returnedAmount = _itemsCollection[item].RemoveAmount(amount);
            if (_itemsCollection[item].Count == 0)
                _itemsCollection.Remove(item);
            return returnedAmount;
        }
        
    }
}
