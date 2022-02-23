using System.Collections.Generic;
using Sheldier.Item;

namespace Sheldier.Actors.Inventory
{
    public class Inventory : IActorsInventory
    {
        public IReadOnlyList<SimpleItem> ItemsCollection => _itemsCollection;
        private List<SimpleItem> _itemsCollection;
        private Actor _owner;

        public void Initialize()
        {
            _itemsCollection = new List<SimpleItem>();
        }

        public bool IsItemExists(SimpleItem item) => _itemsCollection.Contains(item);

        public void AddItem(SimpleItem item)
        {
            var group = item.ItemConfig.ItemGroup;
            item.PutToInventory(_owner, _itemsCollection);
            _owner.Notifier.NotifyAddedItemToInventory(item);
        }

        public void RemoveItem(SimpleItem item)
        {
            if (!IsItemExists(item))
                return;
            item.Drop();
        }

        public void SetOwner(Actor actor)
        {
            _owner = actor;
        }
    }
}
