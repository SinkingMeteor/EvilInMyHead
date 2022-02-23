using System.Collections.Generic;
using Sheldier.Item;

namespace Sheldier.Actors.Inventory
{
    public class Inventory : IActorsInventory
    {
        public IReadOnlyDictionary<ItemConfig, List<SimpleItem>> ItemsCollection => _itemsCollection;
        private Dictionary<ItemConfig, List<SimpleItem>> _itemsCollection;
        private Actor _owner;

        public void Initialize()
        {
            _itemsCollection = new Dictionary<ItemConfig, List<SimpleItem>>();
        }

        public bool IsItemExists(ItemConfig itemConfig)
        {
            return  _itemsCollection.ContainsKey(itemConfig);
        }

        public void AddItem(SimpleItem item)
        {
            var group = item.ItemConfig.ItemGroup;
            if(!IsItemExists(item.ItemConfig))
                _itemsCollection.Add(item.ItemConfig, new List<SimpleItem>());
            item.PutToInventory(_owner, _itemsCollection);
            _owner.Notifier.NotifyAddedItemToInventory(item);
        }

        public int RemoveItem(SimpleItem item, int amount = 1)
        {
            if (!IsItemExists(item.ItemConfig))
                return 0;
            if (!_itemsCollection[item.ItemConfig].Contains(item))
                return 0;
            return item.RemoveItem(_itemsCollection, amount);
        }

        public int RemoveItem(ItemConfig item, int amount = 1)
        {
            if (!IsItemExists(item))
                return 0;
            return _itemsCollection[item][0].RemoveItem(_itemsCollection, amount);
        }
        public void SetOwner(Actor actor)
        {
            _owner = actor;
        }
    }
    
    
}
