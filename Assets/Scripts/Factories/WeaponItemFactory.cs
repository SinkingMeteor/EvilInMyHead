using System;
using System.Collections.Generic;
using Sheldier.Common.Animation;
using Sheldier.Common.Pool;
using Sheldier.Data;
using Sheldier.Item;
using UnityEngine;

namespace Sheldier.Factories
{
    public class WeaponItemFactory : ISubItemFactory
    {
        private readonly IPool<Projectile> _projectilePool;
        private readonly IPool<WeaponBlow> _weaponBlowPool;
        private readonly AssetProvider<Sprite> _spriteLoader;
        private readonly AssetProvider<AnimationData> _animationLoader;

        private readonly Database<ItemStaticWeaponData> _staticWeaponDatabse;
        private readonly Database<ItemDynamicConfigData> _dynamicConfigDatabase;
        private readonly Database<ItemDynamicWeaponData> _dynamicWeaponDatabase;

        private Dictionary<string, GunWeapon> _gunsCollection;

        public WeaponItemFactory(IPool<Projectile> projectilePool, IPool<WeaponBlow> weaponBlowPool, AssetProvider<AnimationData> animationLoader, AssetProvider<Sprite> spriteLoader,
            Database<ItemStaticWeaponData> staticWeaponDatabse, Database<ItemDynamicWeaponData> dynamicWeaponDatabase, Database<ItemDynamicConfigData> dynamicConfigDatabase)
        {
            _dynamicConfigDatabase = dynamicConfigDatabase;
            _staticWeaponDatabse = staticWeaponDatabse;
            _dynamicWeaponDatabase = dynamicWeaponDatabase;
            _animationLoader = animationLoader;
            _spriteLoader = spriteLoader;
            _weaponBlowPool = weaponBlowPool;
            _projectilePool = projectilePool;

            _gunsCollection = new Dictionary<string, GunWeapon>()
            {
                {"Pistol", new GunWeapon(Guid.NewGuid().ToString(), _projectilePool, _weaponBlowPool, _animationLoader, _spriteLoader, _dynamicConfigDatabase, _dynamicWeaponDatabase)}
            };
        }

        public void CreateItemData(string guid, string typeName)
        {
            ItemStaticWeaponData staticWeaponData = _staticWeaponDatabse.Get(typeName);
            ItemDynamicWeaponData dynamicWeaponData = new ItemDynamicWeaponData(guid, staticWeaponData);
            _dynamicWeaponDatabase.Add(dynamicWeaponData);
        }

        public SimpleItem GetItem(string guid, string typeName)
        {
            ItemDynamicWeaponData dynamicWeaponData = _dynamicWeaponDatabase.Get(guid);
             GunWeapon gunWeapon = _gunsCollection[dynamicWeaponData.TypeName].CleanClone(guid);
             return gunWeapon;
        }
    }
}