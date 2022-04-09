using Sheldier.Common;
using Sheldier.Constants;
using Sheldier.Data;
using Sheldier.Item;
using UnityEngine;
using Zenject;

namespace Sheldier.Setup
{
    public class ItemStaticDataLoader
    {
        private Database<ItemStaticConfigData> _staticConfigDatabase;
        private Database<ItemStaticWeaponData> _staticWeaponDatabase;
        private Database<ItemStaticProjectileData> _staticProjectileDatabase;
        private Database<ItemStaticInventorySlotData> _staticInventorySlotDatabase;
        private AssetProvider<TextAsset> _dataLoader;

        [Inject]
        private void InjectDependencies(Database<ItemStaticConfigData> staticConfigDatabase, Database<ItemStaticWeaponData> staticWeaponDatabase,
            Database<ItemStaticProjectileData> staticProjectileDatabase, Database<ItemStaticInventorySlotData> staticInventorySlotDatabase, AssetProvider<TextAsset> dataLoader)
        {
            _dataLoader = dataLoader;
            _staticInventorySlotDatabase = staticInventorySlotDatabase;
            _staticConfigDatabase = staticConfigDatabase;
            _staticWeaponDatabase = staticWeaponDatabase;
            _staticProjectileDatabase = staticProjectileDatabase;
        }
        
        public void LoadStaticData()
        {
            _staticConfigDatabase.FillDatabase(_dataLoader.Get(AssetPathConstants.ITEM_CONFIG));
            _staticWeaponDatabase.FillDatabase(_dataLoader.Get(AssetPathConstants.WEAPON_CONFIG));
            _staticProjectileDatabase.FillDatabase(_dataLoader.Get(AssetPathConstants.PROJECTILE_CONFIG));
            _staticInventorySlotDatabase.FillDatabase(_dataLoader.Get(AssetPathConstants.INVENTORY_SLOT));
        }
    }
}