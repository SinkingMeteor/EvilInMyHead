using System.Collections.Generic;
using Sheldier.Factories;
using Sheldier.Item;

namespace Sheldier.Actors.Inventory
{
    public class WeaponInventorySubModule : InventorySubModule<GunWeapon>
    {
        private readonly ActorNotifyModule _actorNotifyModule;

        public WeaponInventorySubModule(ItemFactory itemFactory, ActorNotifyModule actorNotifyModule) : base(itemFactory)
        {
            _actorNotifyModule = actorNotifyModule;
        }

        public override void AddItem(ItemConfig config)
        {
            GunWeapon weapon = _itemFactory.WeaponItemFactory.GetItem(config);
            if (IsItemExists(config))
                _itemsCollection[config].Add(weapon);
            else
                _itemsCollection.Add(config, new List<GunWeapon>{weapon});
            _actorNotifyModule.NotifyWeaponPickUp(weapon);
        }

        public override void RemoveItem(GunWeapon item)
        {
            _itemsCollection[item.ItemConfig].Remove(item);
            _actorNotifyModule.NotifyWeaponDropped(item);
        }
    }
}