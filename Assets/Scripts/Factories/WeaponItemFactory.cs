using System;
using Sheldier.Common.Pool;
using Sheldier.Item;

namespace Sheldier.Factories
{
    public class WeaponItemFactory
    {
        private readonly ItemMap _itemMap;
        private readonly ProjectilePool _projectilePool;

        public WeaponItemFactory(ItemMap itemMap, ProjectilePool projectilePool)
        {
            _projectilePool = projectilePool;
            _itemMap = itemMap;
        }
        
        public GunWeapon GetItem(ItemConfig itemConfig)
        {
            var itemType = itemConfig.ItemType;
            return itemConfig.ItemType switch
            {
                ItemType.Pistol => new GunWeapon(_itemMap.WeaponMap[ItemType.Pistol], _projectilePool),
                _ => throw new ArgumentOutOfRangeException(nameof(itemType), itemType, null)
            };
        }
    }
}