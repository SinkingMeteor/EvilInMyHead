using System.Collections;
using Sheldier.Actors;
using Sheldier.Common.Pool;
using UnityEngine;

namespace Sheldier.Item
{
    public class GunWeapon : IItem
    {
        public WeaponConfig WeaponConfig => _weaponConfig;
        public ItemConfig ItemConfig => _weaponConfig;

        private WeaponConfig _weaponConfig;
        private ProjectilePool _projectilePool;
        private HandView _weaponView;
        private float _kickbackAngle;
        private float _kickbackPower;
        
        private Coroutine _reduceCoroutine;

        public GunWeapon(WeaponConfig weaponConfig, ProjectilePool projectilePool)
        {
            _projectilePool = projectilePool;
            _weaponConfig = weaponConfig;
        }

        public void SetWeaponView(HandView weaponView)
        {
            _weaponView = weaponView;
        }
        public void Shoot()
        {
            Projectile projectile = _projectilePool.GetFromPool();
            CreateKickback();
            projectile.transform.position = _weaponView.Aim.position;
            var globalDir = (_weaponView.FarPoint.position - _weaponView.NearPoint.position).normalized;
            projectile.SetDirection(new Vector2(globalDir.x, globalDir.y));
            projectile.SetRotation(_weaponView.transform.rotation);
            projectile.SetConfig(_weaponConfig.ProjectileConfig);
        }

        public void Reload()
        {
            Debug.Log("Pistol reloads");
        }

        public float GetHandRotation(float angle)
        {
            angle = Mathf.Lerp(0.0f,_kickbackAngle,  _kickbackPower) + angle;
            return angle;
        }
        private void CreateKickback()
        {
            _kickbackAngle = Random.Range(-10.0f, 10.0f);
            if (_reduceCoroutine != null)
                _weaponView.StopCoroutine(_reduceCoroutine);
            _reduceCoroutine = _weaponView.StartCoroutine(ReduceKickbackPower());
        }
        private IEnumerator ReduceKickbackPower()
        {
            _kickbackPower = 1.0f;
            while (_kickbackPower > 0.0f)
            {
                _kickbackPower -= Time.deltaTime * 2;
                yield return null;
            }
        }
    }
}