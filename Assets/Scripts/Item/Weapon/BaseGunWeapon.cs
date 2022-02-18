using System;
using Sheldier.Actors;
using UnityEngine;

namespace Sheldier.Item
{
    public class BaseGunWeapon : PickupObject, IGunWeapon, IEquippable
    {
        public Transform Transform => transform;
        public WeaponConfig WeaponConfig => weaponConfig;

        [SerializeField] private WeaponConfig weaponConfig;
        private IActorsHand _itemKeeper;


        public void Shoot(Vector2 direction)
        {
            Debug.Log("Pistol shoots");
        }

        public void Reload()
        {
            Debug.Log("Pistol reloads");
        }

        public void OnEquip(IActorsHand itemKeeper)
        {
            _itemKeeper = itemKeeper;
            _itemKeeper.OnHandAttack += Shoot;
            _itemKeeper.OnHandReload += Reload;
        }

        public void OnUnEquip()
        {
            _itemKeeper.OnHandAttack -= Shoot;
            _itemKeeper.OnHandReload -= Reload;
            _itemKeeper = null;
        }


    }
}