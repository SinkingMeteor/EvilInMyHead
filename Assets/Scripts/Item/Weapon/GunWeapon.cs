using System;
using Sheldier.Actors;
using Sheldier.Actors.Hand;
using Sheldier.Common.Pool;
using UnityEngine;

namespace Sheldier.Item
{
    public class GunWeapon : SimpleItem
    {
        private readonly WeaponConfig _weaponConfig;
        private readonly WeaponShootModule _shootModule;
        private readonly WeaponReloadModule _reloadModule;

        private HandView _weaponView;

        private Actor _owner;

        private int _ammoLeft;

        public GunWeapon(WeaponConfig weaponConfig, ProjectilePool projectilePool, WeaponBlowPool weaponBlowPool) : base(weaponConfig)
        {
            _weaponConfig = weaponConfig;
            _ammoLeft = 4;
            _reloadModule = new WeaponReloadModule(_weaponConfig);
            _shootModule = new WeaponShootModule(_weaponConfig, projectilePool, weaponBlowPool);
        }

        public override void Equip(HandView handView, Actor owner)
        {
            _owner = owner;
            _weaponView = handView;
            _weaponView.AddItem(_itemConfig.Icon);
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
            var dir = _owner.InputController.GetNonNormalizedCursorDirectionByTransform(_weaponView.transform.position);
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
            if (!_owner.InventoryModule.IsItemExists(_weaponConfig.RequiredAmmoType))
                return;
            if (_ammoLeft >= _weaponConfig.Capacity)
                return;
            if (_reloadModule.IsReloading)
                return;
            _reloadModule.StartReloading(AddAmmoAfterReloading);
        }
        private void AddAmmoAfterReloading()
        {
            int newAmmo = _owner.InventoryModule.RemoveItem(_weaponConfig.RequiredAmmoType, _weaponConfig.Capacity - _ammoLeft);
            _ammoLeft += newAmmo;
        }
    }
}