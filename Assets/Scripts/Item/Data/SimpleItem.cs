using System.Collections.Generic;
using Sheldier.Actors;
using Sheldier.Actors.Hand;

namespace Sheldier.Item
{
    public abstract class SimpleItem
    {
        public ItemConfig ItemConfig => _itemConfig;
        public virtual bool IsEquippable => true;

        protected readonly ItemConfig _itemConfig;

        public SimpleItem(ItemConfig itemConfig)
        {
            _itemConfig = itemConfig;
        }

        public abstract void PutToInventory(Actor owner, List<SimpleItem> itemsCollection);

        public abstract void Drop();

        public abstract void Equip(HandView handView);

        public abstract void Unequip();
    }
}