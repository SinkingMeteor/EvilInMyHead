using System;
using Sheldier.Common.Pool;
using Sheldier.Item;
using Zenject;

namespace Sheldier.Factories
{
    public class ItemFactory
    {
        
        private ItemMap _itemMap;
        private WeaponItemFactory _weaponItemFactory;
        private ProjectilePool _projectilePool;
        private WeaponBlowPool _weaponBlowPool;

        public void Initialize()
        {
            _weaponItemFactory = new WeaponItemFactory(_itemMap, _projectilePool, _weaponBlowPool);
        }
        
        [Inject]
        private void InjectDependencies(ItemMap itemMap, ProjectilePool projectilePool, WeaponBlowPool weaponBlowPool)
        {
            _weaponBlowPool = weaponBlowPool;
            _projectilePool = projectilePool;
            _itemMap = itemMap;
        }

        public SimpleItem GetItem(ItemConfig itemConfig)
        {
            var group = itemConfig.ItemGroup;
            return group switch
            {
                ItemGroup.Weapon => _weaponItemFactory.GetItem(itemConfig),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
    }
}