using System.Collections;
using Sheldier.Actors.Hand;
using Sheldier.Common.Pool;
using UnityEngine;

namespace Sheldier.Item
{
    public class GunWeapon : IItem
    {
        public WeaponConfig WeaponConfig => _weaponConfig;
        public ItemConfig ItemConfig => _weaponConfig;

        private readonly WeaponBlowPool _weaponBlowPool;
        private readonly WeaponConfig _weaponConfig;
        private readonly ProjectilePool _projectilePool;
        
        private HandView _weaponView;
        private float _kickbackAngle;
        private float _kickbackPower;

        private Coroutine _reduceCoroutine;
        private GameObject _aim;

        public GunWeapon(WeaponConfig weaponConfig, ProjectilePool projectilePool, WeaponBlowPool weaponBlowPool)
        {
            _weaponBlowPool = weaponBlowPool;
            _projectilePool = projectilePool;
            _weaponConfig = weaponConfig;
        }

        public void SetWeaponView(HandView weaponView)
        {
            _weaponView = weaponView;
            _aim = new GameObject("Aim");
            _aim.transform.SetParent(weaponView.transform);
            _aim.transform.localPosition = _weaponConfig.AimLocalPosition;
            _aim.transform.localRotation = Quaternion.identity;
            _aim.transform.localScale = Vector3.one;
        }
        public void Shoot(Vector2 direction)
        {
            Projectile projectile = _projectilePool.GetFromPool();
            CreateKickback();
            var position = _aim.transform.position;
            var rotation = _aim.transform.rotation;
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
        }

        public void Reload()
        {
            Debug.Log("Pistol reloads");
        }

        public void Unequip()
        {
            GameObject.Destroy(_aim);
        }
        public float GetHandRotation(float angle)
        {
            angle = Mathf.Lerp(0.0f,_kickbackAngle,  _kickbackPower) + angle;
            return angle;
        }
        private void CreateKickback()
        {
            _kickbackAngle = Random.value > 0.5 ?  Random.Range(-15.0f, -10.0f) : Random.Range(10.0f, 15.0f);
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