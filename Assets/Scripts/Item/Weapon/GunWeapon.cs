using System.Collections;
using System.Collections.Generic;
using Sheldier.Actors;
using Sheldier.Actors.Hand;
using Sheldier.Common.Pool;
using UnityEngine;

namespace Sheldier.Item
{
    public class GunWeapon : SimpleItem
    {
        private readonly WeaponBlowPool _weaponBlowPool;
        private readonly WeaponConfig _weaponConfig;
        private readonly ProjectilePool _projectilePool;
        
        private HandView _weaponView;
        private float _kickbackAngle;
        private float _kickbackPower;

        private Coroutine _reduceCoroutine;
        private GameObject _aim;
        private Actor _owner;

        private int _ammoLeft;
        
        public GunWeapon(WeaponConfig weaponConfig, ProjectilePool projectilePool, WeaponBlowPool weaponBlowPool) : base(weaponConfig)
        {
            _weaponBlowPool = weaponBlowPool;
            _projectilePool = projectilePool;
            _weaponConfig = weaponConfig;
            _ammoLeft = 3;
        }


        public void Shoot(Vector2 direction)
        {
            if (_ammoLeft == 0)
                return;

            _ammoLeft -= 1;
            Debug.Log("Ammo left " + _ammoLeft);
            direction = Quaternion.AngleAxis(Random.Range(-5.0f, 5.0f), Vector3.forward) * direction;
            
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
            if (!_owner.InventoryModule.IsItemExists(_weaponConfig.RequiredAmmoType))
                return;
            if (_ammoLeft == _weaponConfig.Capacity)
                return;
            int newAmmo = _owner.InventoryModule.RemoveItem(_weaponConfig.RequiredAmmoType, _weaponConfig.Capacity - _ammoLeft);
            Debug.Log("Ammo added " + newAmmo);
            _ammoLeft += newAmmo;
        }
        public override void Equip(HandView handView)
        {
            _weaponView = handView;
            _aim = new GameObject("Aim");
            _aim.transform.SetParent(handView.transform);
            _aim.transform.localPosition = _weaponConfig.AimLocalPosition;
            _aim.transform.localRotation = Quaternion.identity;
            _aim.transform.localScale = Vector3.one;
            
            _owner.Notifier.OnActorAttacks += Shoot;
            _owner.Notifier.OnActorReloads += Reload;

        }
        public override void Unequip()
        {
            _owner.Notifier.OnActorAttacks -= Shoot;
            _owner.Notifier.OnActorReloads -= Reload;
            
            GameObject.Destroy(_aim);
        }
        public override void PutToInventory(Actor owner, Dictionary<ItemConfig, List<SimpleItem>> itemsCollection)
        {
            _owner = owner;
            itemsCollection[_itemConfig].Add(this);
        }

        public override int RemoveItem(Dictionary<ItemConfig, List<SimpleItem>> itemsCollection, int amount)
        {
            Drop();
            return 1;
        }

        protected override void Drop()
        {
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