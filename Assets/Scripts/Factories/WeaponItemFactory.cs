using System;
using Sheldier.Common.Pool;
using Sheldier.Item;

namespace Sheldier.Factories
{
    public class WeaponItemFactory
    {
        private readonly ItemMap _itemMap;
        private readonly ProjectilePool _projectilePool;
        private readonly WeaponBlowPool _weaponBlowPool;

        public WeaponItemFactory(ItemMap itemMap, ProjectilePool projectilePool, WeaponBlowPool weaponBlowPool)
        {
            _weaponBlowPool = weaponBlowPool;
            _projectilePool = projectilePool;
            _itemMap = itemMap;
        }
        
        public GunWeapon GetItem(ItemConfig itemConfig)
        {
            var itemType = itemConfig.ItemType;
            return itemConfig.ItemType switch
            {
                ItemType.Pistol => new GunWeapon(_itemMap.WeaponMap[ItemType.Pistol], _projectilePool, _weaponBlowPool),
                _ => throw new ArgumentOutOfRangeException(nameof(itemType), itemType, null)
            };
        }
    }
}