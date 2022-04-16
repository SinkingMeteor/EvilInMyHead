using System;
using System.Collections.Generic;
using Sheldier.Common.Animation;
using Sheldier.Common.Pool;
using Sheldier.Data;
using Sheldier.Item;
using UnityEngine;
using Zenject;

namespace Sheldier.Factories
{
    public class ItemFactory
    {
        
        private WeaponItemFactory _weaponItemFactory;
        private AmmoItemFactory _ammoItemFactory;
        private IPool<Projectile> _projectilePool;
        private IPool<WeaponBlow> _weaponBlowPool;
        private AssetProvider<Sprite> _spriteLoader;
        private AssetProvider<AnimationData> _animationLoader;

        private Database<ItemStaticWeaponData> _staticWeaponDatabase;
        private Database<ItemDynamicWeaponData> _dynamicWeaponDatabase;
        private Database<ItemDynamicConfigData> _dynamicConfigDatabase;
        private Database<ItemStaticConfigData> _staticConfigDatabase;
        private Database<SimpleItem> _simpleItemDatabase;

        private Dictionary<string, ISubItemFactory> _subFactories;

        public void Initialize()
        {
            _subFactories = new Dictionary<string, ISubItemFactory>()
            {
                {"Weapon", new WeaponItemFactory(_projectilePool, _weaponBlowPool, _animationLoader, _spriteLoader,
                    _staticWeaponDatabase, _dynamicWeaponDatabase, _dynamicConfigDatabase)},
                {"Ammo", new AmmoItemFactory()}
            };
        }
        [Inject]
        public void InjectDependencies(IPool<Projectile> projectilePool, IPool<WeaponBlow> weaponBlowPool, AssetProvider<Sprite> spriteLoader, 
            AssetProvider<AnimationData> animationLoader, Database<ItemStaticWeaponData> staticWeaponDatabase, Database<ItemDynamicWeaponData> dynamicWeaponDatabase, 
            Database<ItemStaticConfigData> staticConfigDatabase, Database<ItemDynamicConfigData> dynamicConfigDatabase, Database<SimpleItem> simpleItemDatabase)
        {
            _simpleItemDatabase = simpleItemDatabase;
            _staticConfigDatabase = staticConfigDatabase;
            _dynamicConfigDatabase = dynamicConfigDatabase;
            _staticWeaponDatabase = staticWeaponDatabase;
            _dynamicWeaponDatabase = dynamicWeaponDatabase;
            _animationLoader = animationLoader;
            _spriteLoader = spriteLoader;
            _weaponBlowPool = weaponBlowPool;
            _projectilePool = projectilePool;
        }

        public ItemDynamicConfigData CreateItem(string typeName, string guid)
        {
            if (_dynamicConfigDatabase.IsItemExists(guid))
                return _dynamicConfigDatabase.Get(guid);
            
            ItemStaticConfigData staticConfigData = _staticConfigDatabase.Get(typeName);
            ItemDynamicConfigData dynamicConfigData = new ItemDynamicConfigData(guid, staticConfigData);
            _dynamicConfigDatabase.Add(dynamicConfigData);
            
            var group = staticConfigData.GroupName;
            _subFactories[group].CreateItemData(guid, typeName);
            return dynamicConfigData;
        }
        
        public SimpleItem GetItem(string guid)
        {
            if (_simpleItemDatabase.IsItemExists(guid))
                return _simpleItemDatabase.Get(guid);
            
            ItemDynamicConfigData dynamicConfigData = _dynamicConfigDatabase.Get(guid);
            SimpleItem item = _subFactories[dynamicConfigData.GroupName].GetItem(guid, dynamicConfigData.TypeName);
            _simpleItemDatabase.Add(item);
            item.Initialize();
            return item;
        }
        
    }
}