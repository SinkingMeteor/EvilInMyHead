using System;
using Sheldier.Actors;
using Sheldier.Actors.Hand;
using Sheldier.Common.Animation;
using Sheldier.Common.Pool;
using Sheldier.Constants;
using Sheldier.Data;
using UnityEngine;

namespace Sheldier.Item
{
    public class GunWeapon : SimpleItem
    {
        private readonly Database<ItemDynamicWeaponData> _dynamicWeaponDatabase;
        private readonly Database<ItemDynamicConfigData> _dynamicConfigDatabase;
        private readonly AssetProvider<AnimationData> _animationLoader;
        private readonly AssetProvider<Sprite> _spriteLoader;
        private readonly IPool<Projectile> _projectilePool;
        private readonly IPool<WeaponBlow> _weaponBlowPool;

        private WeaponShootModule _shootModule;
        private WeaponReloadModule _reloadModule;
        private IHandView _weaponView;

        private Actor _owner;
        
        private ItemDynamicConfigData _dynamicConfigData;
        private ItemDynamicWeaponData _dynamicWeaponData;


        public GunWeapon(string id,
                         IPool<Projectile> projectilePool,
                         IPool<WeaponBlow> weaponBlowPool,
                         AssetProvider<AnimationData> animationLoader,
                         AssetProvider<Sprite> spriteLoader, 
                         Database<ItemDynamicConfigData> dynamicConfigDatabase,
                         Database<ItemDynamicWeaponData> dynamicWeaponDatabase) : base(id)
        {
            _dynamicConfigDatabase = dynamicConfigDatabase;
            _dynamicWeaponDatabase = dynamicWeaponDatabase;
            _spriteLoader = spriteLoader;
            _animationLoader = animationLoader;
            _projectilePool = projectilePool;
            _weaponBlowPool = weaponBlowPool;
        }
        
        public override void Initialize()
        {
            _dynamicConfigData = _dynamicConfigDatabase.Get(ID);
            _dynamicWeaponData = _dynamicWeaponDatabase.Get(ID);
            _reloadModule = new WeaponReloadModule(_dynamicWeaponData, _animationLoader);
            _shootModule = new WeaponShootModule(_dynamicWeaponData, _projectilePool, _weaponBlowPool);

        }

        public override void Equip(IHandView handView, Actor owner)
        {
            _owner = owner;
            _weaponView = handView;
            _weaponView.AddItem(_spriteLoader.Get( _dynamicConfigData.GameIcon));
            _reloadModule.SetView(handView);
            _shootModule.SetView(handView);
            _shootModule.CreateAim();
            
            _owner.Notifier.OnActorAttacks += Shoot;
            _owner.Notifier.OnActorReloads += Reload;

        }
        public override void Unequip()
        {
            _owner.Notifier.OnActorAttacks -= Shoot;
            _owner.Notifier.OnActorReloads -= Reload;
            _weaponView.ClearItem();
            _reloadModule.Dispose();
            _shootModule.Dispose();
            _owner = null;
            _weaponView = null;
        }

    
        public override void Drop()
        {
        }

        public override Vector2 GetRotateDirection()
        {
            var dir = _owner.InputController.GetNonNormalizedCursorDirectionByTransform(_weaponView.Transform.position).normalized;
            dir = Quaternion.Euler(new Vector3(0.0f, 0.0f, _shootModule.KickbackAngle)) * dir;
            return dir;
        }
        private void Shoot()
        {
            if (_dynamicWeaponData.AmmoLeft == 0 || !_shootModule.CanShoot || !_reloadModule.CanShoot)
                return;
            _dynamicWeaponData.AmmoLeft -= 1;
            
            _shootModule.Shoot( _owner.InputController.GetNonNormalizedCursorDirectionByTransform(_shootModule.Aim.position).normalized);
        }

        private void Reload()
        {
            if (!_owner.InventoryModule.IsItemTypeExists(_dynamicWeaponData.RequiredAmmoItemName))
                return;
            if (_dynamicWeaponData.AmmoLeft >= _dynamicWeaponData.Capacity)
                return;
            if (_reloadModule.IsReloading)
                return;
            _reloadModule.StartReloading(AddAmmoAfterReloading);
        }
        private void AddAmmoAfterReloading()
        {
            int newAmmo = _owner.InventoryModule.RemoveItem(_dynamicWeaponData.RequiredAmmoItemName, _dynamicWeaponData.Capacity - _dynamicWeaponData.AmmoLeft).Amount;
            _dynamicWeaponData.AmmoLeft += newAmmo;
        }

        public override string GetExtraInfo()
        {
            return $"{_dynamicWeaponData.AmmoLeft}/{_dynamicWeaponData.Capacity}";
        }

        public GunWeapon CleanClone(string guid) => new GunWeapon(guid, _projectilePool, _weaponBlowPool, _animationLoader, _spriteLoader, _dynamicConfigDatabase, _dynamicWeaponDatabase);
    }
}