using System;
using System.Collections.Generic;
using Sheldier.Common.Pool;
using Sheldier.Data;
using Sheldier.Item;

namespace Sheldier.Factories
{
    public class WeaponItemFactory : ISubItemFactory
    {
        private readonly ProjectilePool _projectilePool;
        private readonly WeaponBlowPool _weaponBlowPool;
        private readonly SpriteLoader _spriteLoader;
        private readonly AnimationLoader _animationLoader;

        private readonly Database<ItemStaticWeaponData> _staticWeaponDatabse;
        private readonly Database<ItemDynamicWeaponData> _dynamicWeaponDatabase;
        
        private Dictionary<string, GunWeapon> _gunsCollection;

        public WeaponItemFactory(ProjectilePool projectilePool, WeaponBlowPool weaponBlowPool, AnimationLoader animationLoader, SpriteLoader spriteLoader,
            Database<ItemStaticWeaponData> staticWeaponDatabse, Database<ItemDynamicWeaponData> dynamicWeaponDatabase)
        {
            _staticWeaponDatabse = staticWeaponDatabse;
            _dynamicWeaponDatabase = dynamicWeaponDatabase;
            _animationLoader = animationLoader;
            _spriteLoader = spriteLoader;
            _weaponBlowPool = weaponBlowPool;
            _projectilePool = projectilePool;

            _gunsCollection = new Dictionary<string, GunWeapon>()
            {
                {"Pistol", new GunWeapon(_projectilePool, _weaponBlowPool, _animationLoader, _spriteLoader)}
            };
        }

        public void CreateItemData(string guid, string typeName)
        {
            ItemStaticWeaponData staticWeaponData = _staticWeaponDatabse.Get(typeName);
            ItemDynamicWeaponData dynamicWeaponData = new ItemDynamicWeaponData(guid, staticWeaponData);
            _dynamicWeaponDatabase.Add(guid, dynamicWeaponData);
        }

        public SimpleItem GetItem(string guid)
        {
            ItemDynamicWeaponData dynamicWeaponData = _dynamicWeaponDatabase.Get(guid);
             GunWeapon gunWeapon = _gunsCollection[dynamicWeaponData.ItemName].CleanClone();
             gunWeapon.SetDynamicWeaponData(dynamicWeaponData);
             return gunWeapon;
        }
    }
}