using System;
using Sheldier.Item;

namespace Sheldier.Factories
{
    public class WeaponItemFactory
    {
        private readonly ItemMap _itemMap;

        public WeaponItemFactory(ItemMap itemMap)
        {
            _itemMap = itemMap;
        }
        
        public GunWeapon GetItem(ItemConfig itemConfig)
        {
            var itemType = itemConfig.ItemType;
            return itemConfig.ItemType switch
            {
                ItemType.Pistol => new GunWeapon(_itemMap.WeaponMap[ItemType.Pistol]),
                _ => throw new ArgumentOutOfRangeException(nameof(itemType), itemType, null)
            };
        }
    }
}