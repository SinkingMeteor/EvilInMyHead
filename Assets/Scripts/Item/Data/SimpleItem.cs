using System.Collections.Generic;
using Sheldier.Actors;
using Sheldier.Actors.Hand;
using Sheldier.Common.Utilities;

namespace Sheldier.Item
{
    public abstract class SimpleItem
    {
        public ItemConfig ItemConfig => _itemConfig;
        public virtual bool IsEquippable => true;
        public Counter ItemAmount => _itemAmount;

        protected Counter _itemAmount;

        protected readonly ItemConfig _itemConfig;

        public SimpleItem(ItemConfig itemConfig)
        {
            _itemConfig = itemConfig;
            _itemAmount = new Counter(1);
        }

        public abstract void PutToInventory(Actor owner, Dictionary<ItemConfig, List<SimpleItem>> itemsCollection);

        public abstract int RemoveItem(Dictionary<ItemConfig, List<SimpleItem>> itemsCollection, int amount);
        protected abstract void Drop();

        public abstract void Equip(HandView handView);

        public abstract void Unequip();
    }
}