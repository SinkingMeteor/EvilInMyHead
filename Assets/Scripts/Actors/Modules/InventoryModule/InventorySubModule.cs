using System.Collections.Generic;
using Sheldier.Factories;
using Sheldier.Item;

namespace Sheldier.Actors.Inventory
{
    public abstract class InventorySubModule<T> where T : IItem
    {
        public Dictionary<ItemConfig, List<T>> ItemCollection => _itemsCollection;

        protected Dictionary<ItemConfig, List<T>> _itemsCollection;
        protected readonly ItemFactory _itemFactory;
        protected InventorySubModule(ItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
            _itemsCollection = new Dictionary<ItemConfig, List<T>>();
        }

        public abstract void AddItem(ItemConfig config);

        public abstract void RemoveItem(T item);
        public bool IsItemExists(ItemConfig itemConfig) => _itemsCollection.ContainsKey(itemConfig);
        
    }
}