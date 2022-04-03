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
        private ItemDynamicWeaponData _dynamicWeaponData;
        
        private readonly WeaponShootModule _shootModule;
        private readonly WeaponReloadModule _reloadModule;
        private readonly AnimationLoader _animationLoader;
        private readonly ProjectilePool _projectilePool;
        private readonly SpriteLoader _spriteLoader;
        private readonly WeaponBlowPool _weaponBlowPool;

        private HandView _weaponView;

        private Actor _owner;

        private int _ammoLeft;

        public GunWeapon(ProjectilePool projectilePool, WeaponBlowPool weaponBlowPool, AnimationLoader animationLoader, SpriteLoader spriteLoader)
        {
            _spriteLoader = spriteLoader;
            _animationLoader = animationLoader;
            _projectilePool = projectilePool;
            _weaponBlowPool = weaponBlowPool;
            _reloadModule = new WeaponReloadModule(animationLoader);
            _shootModule = new WeaponShootModule(projectilePool, weaponBlowPool);
        }

        public void SetDynamicWeaponData(ItemDynamicWeaponData dynamicWeaponData)
        {
            _dynamicWeaponData = dynamicWeaponData;
        }
        
        public override void Equip(HandView handView, Actor owner)
        {
            _owner = owner;
            _weaponView = handView;
            _weaponView.AddItem(_spriteLoader.Get(_itemConfig.GameIcon, TextDataConstants.ITEM_ICONS_DIRECTORY));
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
            var dir = _owner.InputController.GetNonNormalizedCursorDirectionByTransform(_weaponView.transform.position).normalized;
            dir = Quaternion.Euler(new Vector3(0.0f, 0.0f, _shootModule.KickbackAngle)) * dir;
            return dir;
        }
        private void Shoot()
        {
            if (_ammoLeft == 0 || !_shootModule.CanShoot || !_reloadModule.CanShoot)
                return;
            _ammoLeft -= 1;
            
            _shootModule.Shoot( _owner.InputController.GetNonNormalizedCursorDirectionByTransform(_shootModule.Aim.position).normalized);
        }

        private void Reload()
        {
            if (!_owner.InventoryModule.IsItemTypeExists(_dynamicWeaponData.RequiredAmmoItemName))
                return;
            if (_ammoLeft >= _dynamicWeaponData.Capacity)
                return;
            if (_reloadModule.IsReloading)
                return;
            _reloadModule.StartReloading(AddAmmoAfterReloading);
        }
        private void AddAmmoAfterReloading()
        {
            int newAmmo = _owner.InventoryModule.RemoveItem(_dynamicWeaponData.RequiredAmmoItemName, _dynamicWeaponData.Capacity - _ammoLeft).Amount;
            _ammoLeft += newAmmo;
        }

        public override string GetExtraInfo()
        {
            return $"{_ammoLeft}/{_dynamicWeaponData.Capacity}";
        }

        public GunWeapon CleanClone() => new GunWeapon(_projectilePool, _weaponBlowPool, _animationLoader, _spriteLoader);
    }
}