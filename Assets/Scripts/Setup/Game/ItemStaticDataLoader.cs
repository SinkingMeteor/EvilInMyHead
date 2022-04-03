using Sheldier.Common;
using Sheldier.Constants;
using Sheldier.Data;
using Sheldier.Item;
using Zenject;

namespace Sheldier.Setup
{
    public class ItemStaticDataLoader
    {
        private Database<ItemStaticConfigData> _staticConfigDatabase;
        private Database<ItemStaticWeaponData> _staticWeaponDatabase;
        private Database<ItemStaticProjectileData> _staticProjectileDatabase;

        [Inject]
        private void InjectDependencies(Database<ItemStaticConfigData> staticConfigDatabase, Database<ItemStaticWeaponData> staticWeaponDatabase,
            Database<ItemStaticProjectileData> staticProjectileDatabase)
        {
            _staticConfigDatabase = staticConfigDatabase;
            _staticWeaponDatabase = staticWeaponDatabase;
            _staticProjectileDatabase = staticProjectileDatabase;
        }
        
        public void LoadStaticData()
        {
            _staticConfigDatabase.FillDatabase(TextDataConstants.ITEM_CONFIG);
            _staticWeaponDatabase.FillDatabase(TextDataConstants.WEAPON_CONFIG);
            _staticProjectileDatabase.FillDatabase(TextDataConstants.PROJECTILE_CONFIG);
        }
    }
}