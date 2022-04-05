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
        private IHandView _weaponView;

        private float _kickbackAngle;
        private float _kickbackPower;

        private readonly IPool<Projectile> _projectilePool;
        private readonly IPool<WeaponBlow> _weaponBlowPool;
        
        private ItemDynamicWeaponData _weaponConfig;
        private WaitForSeconds _shootCooldown;
        private GameObject _aim;


        public WeaponShootModule(ItemDynamicWeaponData staticWeaponData, IPool<Projectile> projectilePool, IPool<WeaponBlow> weaponBlowPool)
        {
            _weaponConfig = staticWeaponData;
            _projectilePool = projectilePool;
            _weaponBlowPool = weaponBlowPool;
            _shootCooldown = new WaitForSeconds(_weaponConfig.FireRate);
            _canShoot = true;
        }

        public void SetView(IHandView weaponView)
        {
            _weaponView = weaponView;
        }
        
        public void CreateAim()
        {
            _aim = new GameObject("Aim");
            _aim.transform.SetParent(_weaponView.Transform);
            _aim.transform.localPosition = new Vector3(_weaponConfig.AimLocalX, _weaponConfig.AimLocalY, 0.0f);
            _aim.transform.localRotation = Quaternion.Euler(Vector3.zero);
            _aim.transform.localScale = Vector3.one;
        }
        public void Shoot(Vector2 direction)
        {
            direction = Quaternion.AngleAxis(Random.Range(-5.0f, 5.0f), Vector3.forward) * direction;
            
            Projectile projectile = _projectilePool.GetFromPool();
            CreateKickback();
            var position = _aim.transform.position;
            var rotation = _weaponView.Transform.rotation;
            var scale = _aim.transform.lossyScale;

            projectile.transform.position = position;
            projectile.SetDirection(direction);
            projectile.SetRotation(rotation);
            projectile.Transform.localScale = scale;
            projectile.SetData(_weaponConfig.ProjectileName);

            WeaponBlow blow = _weaponBlowPool.GetFromPool();
            blow.transform.position = position;
            blow.transform.rotation = rotation;
            blow.transform.localScale = scale;
            blow.SetAnimation(_weaponConfig.BlowAnimation);

            _shootCooldownCoroutine = _weaponView.Behaviour.StartCoroutine(ShootCooldownCoroutine());
        }
        private void CreateKickback()
        {
            _kickbackAngle = Random.value > 0.5 ?  Random.Range(-20.0f, -10.0f) : Random.Range(10.0f, 20.0f);
            if (_reduceKickbackCoroutine != null)
                _weaponView.Behaviour.StopCoroutine(_reduceKickbackCoroutine);
            _reduceKickbackCoroutine = _weaponView.Behaviour.StartCoroutine(ReduceKickbackPower());
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
                _weaponView.Behaviour.StopCoroutine(_reduceKickbackCoroutine);
            if(_shootCooldownCoroutine != null)
                _weaponView.Behaviour.StopCoroutine(_shootCooldownCoroutine);
            GameObject.Destroy(_aim);
            _weaponView = null;
        }
    }
}