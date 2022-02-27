using System;
using System.Collections;
using Sheldier.Actors;
using Sheldier.Actors.Hand;
using Sheldier.Common.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

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
            _shootModule = new WeaponShootModule(weaponConfig, projectilePool, weaponBlowPool);
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
            if (dir.magnitude < 0.2f)
                return Vector2.zero;
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

            _reloadModule.StartReloading(AddAmmoAfterReloading);
        }
        private void AddAmmoAfterReloading()
        {
            int newAmmo = _owner.InventoryModule.RemoveItem(_weaponConfig.RequiredAmmoType, _weaponConfig.Capacity - _ammoLeft);
            _ammoLeft += newAmmo;
        }
    }

    public class WeaponShootModule
    {
        public Transform Aim => _aim.transform;
        public bool CanShoot => _canShoot;
        public float KickbackAngle => _kickbackAngle * _kickbackPower;

        private bool _canShoot;
        
        private Coroutine _reduceKickbackCoroutine;
        private Coroutine _shootCooldownCoroutine;
        private HandView _weaponView;

        private float _kickbackAngle;
        private float _kickbackPower;

        private readonly WeaponConfig _weaponConfig;
        private readonly ProjectilePool _projectilePool;
        private readonly WeaponBlowPool _weaponBlowPool;
        private readonly WaitForSeconds _shootCooldown;
        private GameObject _aim;


        public WeaponShootModule(WeaponConfig weaponConfig, ProjectilePool projectilePool, WeaponBlowPool weaponBlowPool)
        {
            _weaponConfig = weaponConfig;
            _projectilePool = projectilePool;
            _weaponBlowPool = weaponBlowPool;
            _canShoot = true;
            _shootCooldown = new WaitForSeconds(_weaponConfig.FireRate);
        }

        public void SetView(HandView weaponView)
        {
            _weaponView = weaponView;
        }
        
        public void CreateAim()
        {
            _aim = new GameObject("Aim");
            _aim.transform.SetParent(_weaponView.transform);
            _aim.transform.localPosition = _weaponConfig.AimLocalPosition;
            _aim.transform.localRotation = Quaternion.Euler(Vector3.zero);
            _aim.transform.localScale = Vector3.one;
        }
        public void Shoot(Vector2 direction)
        {
            direction = Quaternion.AngleAxis(Random.Range(-5.0f, 5.0f), Vector3.forward) * direction;
            
            Projectile projectile = _projectilePool.GetFromPool();
            CreateKickback();
            var position = _aim.transform.position;
            var rotation = _weaponView.transform.rotation;
            var scale = _aim.transform.lossyScale;

            projectile.transform.position = position;
            projectile.SetDirection(direction);
            projectile.SetRotation(rotation);
            projectile.Transform.localScale = scale;
            projectile.SetConfig(_weaponConfig.ProjectileConfig);

            WeaponBlow blow = _weaponBlowPool.GetFromPool();
            blow.transform.position = position;
            blow.transform.rotation = rotation;
            blow.transform.localScale = scale;
            blow.SetAnimation(_weaponConfig.WeaponBlowAnimation);

            _shootCooldownCoroutine = _weaponView.StartCoroutine(ShootCooldownCoroutine());
        }
        private void CreateKickback()
        {
            _kickbackAngle = Random.value > 0.5 ?  Random.Range(-20.0f, -10.0f) : Random.Range(10.0f, 20.0f);
            if (_reduceKickbackCoroutine != null)
                _weaponView.StopCoroutine(_reduceKickbackCoroutine);
            _reduceKickbackCoroutine = _weaponView.StartCoroutine(ReduceKickbackPower());
        }
        private IEnumerator ReduceKickbackPower()
        {
            _kickbackPower = 1.0f;
            while (_kickbackPower > 0.0f)
            {
                _kickbackPower = Mathf.Clamp01(_kickbackPower - Time.deltaTime * 2);
                yield return null;
            }
        }

        private IEnumerator ShootCooldownCoroutine()
        {
            _canShoot = false;
            yield return _shootCooldown;
            _canShoot = true;
        }

        public void Dispose()
        {
            if(_reduceKickbackCoroutine != null)
                _weaponView.StopCoroutine(_reduceKickbackCoroutine);
            if(_shootCooldownCoroutine != null)
                _weaponView.StopCoroutine(_shootCooldownCoroutine);
            GameObject.Destroy(_aim);
            _weaponView = null;
        }
    }
    public class WeaponReloadModule
    {
        public bool CanShoot => !_isReloading;
        
        private readonly WeaponConfig _weaponConfig;
        private Coroutine _reloadingCoroutine;
        private HandView _weaponView;
        private bool _isReloading;
        private bool _canShoot;

        public WeaponReloadModule(WeaponConfig weaponConfig)
        {
            _weaponConfig = weaponConfig;
        }

        public void SetView(HandView handView)
        {
            _weaponView = handView;
        }

        public void StartReloading(Action onComplete) => _reloadingCoroutine = _weaponView.StartCoroutine(ReloadingCoroutine(onComplete));

        public void InterruptReloading()
        {
            if (_reloadingCoroutine == null) return;
            
            _weaponView.StopCoroutine(_reloadingCoroutine);
            _weaponView.Animator.StopPlaying();
        }
        private IEnumerator ReloadingCoroutine(Action onComplete)
        {
            _isReloading = true;
            
            _weaponView.Animator.Play(_weaponConfig.ReloadAnimation);

            yield return new WaitForSeconds(_weaponConfig.ReloadAnimation.Frames.Length / _weaponConfig.ReloadAnimation.FrameRate);

            _isReloading = false;
            Debug.Log("Reloaded");

            onComplete.Invoke();
        }

        public void Dispose()
        {
            InterruptReloading();
            _weaponView = null;
        }
    }
}