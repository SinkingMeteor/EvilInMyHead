using System.Collections;
using Sheldier.Actors.Hand;
using Sheldier.Common.Pool;
using UnityEngine;

namespace Sheldier.Item
{
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
}