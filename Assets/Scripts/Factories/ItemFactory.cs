using Sheldier.Common.Pool;
using Sheldier.Item;
using Zenject;

namespace Sheldier.Factories
{
    public class ItemFactory
    {
        public WeaponItemFactory WeaponItemFactory => _weaponItemFactory;
        
        private ItemMap _itemMap;
        private WeaponItemFactory _weaponItemFactory;
        private ProjectilePool _projectilePool;

        public void Initialize()
        {
            _weaponItemFactory = new WeaponItemFactory(_itemMap, _projectilePool);
        }
        
        [Inject]
        private void InjectDependencies(ItemMap itemMap, ProjectilePool projectilePool)
        {
            _projectilePool = projectilePool;
            _itemMap = itemMap;
        }
        
        
    }
}