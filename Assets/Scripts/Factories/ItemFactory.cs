using Sheldier.Item;
using Zenject;

namespace Sheldier.Factories
{
    public class ItemFactory
    {
        public WeaponItemFactory WeaponItemFactory => _weaponItemFactory;
        
        private ItemMap _itemMap;
        private WeaponItemFactory _weaponItemFactory;

        public void Initialize()
        {
            _weaponItemFactory = new WeaponItemFactory(_itemMap);
        }
        
        [Inject]
        private void InjectDependencies(ItemMap itemMap)
        {
            _itemMap = itemMap;
        }
        
        
    }
}