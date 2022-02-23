using System.Collections.Generic;
using Sheldier.Item;

namespace Sheldier.Actors.Inventory
{
    public class Inventory : IActorsInventory
    {
        public IReadOnlyDictionary<ItemGroup, List<SimpleItem>> ItemsCollection => _itemsCollection;
        private Dictionary<ItemGroup, List<SimpleItem>> _itemsCollection;
        private Actor _owner;

        public void Initialize()
        {
            _itemsCollection = new Dictionary<ItemGroup, List<SimpleItem>>();
        }

        public bool IsItemExists(SimpleItem item)
        {
            var group = item.ItemConfig.ItemGroup;
            if (!_itemsCollection.ContainsKey(group))
                return false;
            if (!_itemsCollection[group].Contains(item))
                return false;
            return true;
        }

        public void AddItem(SimpleItem item)
        {
            var group = item.ItemConfig.ItemGroup;
            if(!_itemsCollection.ContainsKey(item.ItemConfig.ItemGroup))
                _itemsCollection.Add(group, new List<SimpleItem>());
            _itemsCollection[group].Add(item);
                
        }

        public void RemoveItem(SimpleItem item)
        {
            if (!IsItemExists(item))
                return;
            _itemsCollection[item.ItemConfig.ItemGroup].Remove(item);
        }

        public void SetOwner(Actor actor)
        {
            _owner = actor;
        }
    }
}
