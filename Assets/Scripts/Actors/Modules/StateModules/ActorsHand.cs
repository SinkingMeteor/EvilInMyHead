﻿using System;
using Sheldier.Actors.Inventory;
using Sheldier.Item;
using Sheldier.Setup;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorsHand : MonoBehaviour, IExtraActorModule
    {
        public bool IsEquipped => _currentEquippable != null;
        public int Priority => 0;
        
        [SerializeField] private ActorsHandedItemView _actorHandObject;
        
        private ActorTransformHandler _transformHandler;
        private IItem _currentEquippable;
        private ActorNotifyModule _notifier;
        private IHandInputReceiver _currentInputReceiver;
        public void Initialize(IActorModuleCenter moduleCenter)
        {
            _transformHandler = moduleCenter.ActorTransformHandler;
            _notifier = moduleCenter.Notifier;
            _notifier.OnWeaponPickedUp += EquipWeapon;

        }

        public void RotateHand(Vector2 dir)
        {
            var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
            if (!_transformHandler.LooksToRight)
                angle -= 180;
            _actorHandObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        private void SetItemSprite(IItem activeItem)
        {
            var icon = activeItem.ItemConfig.Icon;
            _currentEquippable = activeItem;
            _actorHandObject.AddItem(icon);
        }

        private void EquipWeapon(GunWeapon weapon)
        {
            if (_currentEquippable != null)
                return;
            SetItemSprite(weapon);
            _currentInputReceiver = new WeaponedHandInputReceiver(weapon, _notifier);
        }
        
        public void UnEquip()
        {
            if (_currentEquippable == null)
                return;
            if (_currentInputReceiver == null)
                return;
            _actorHandObject.ClearItem();
            _currentInputReceiver.Dispose();
            _currentInputReceiver = null;
            _currentEquippable = null;
        }

        private void OnDestroy()
        {
            if (!GameGlobalSettings.IsStarted) return;
            _notifier.OnWeaponPickedUp -= EquipWeapon;
        }
    }

    public interface IHandInputReceiver
    {
        void Dispose();
    }
    public class WeaponedHandInputReceiver : IHandInputReceiver
    {
        private GunWeapon _currentWeapon;
        private readonly ActorNotifyModule _notifier;

        public WeaponedHandInputReceiver(GunWeapon weapon, ActorNotifyModule notifier)
        {
            _currentWeapon = weapon;
            _notifier = notifier;
            notifier.OnActorAttacks += AttackByWeapon;
            notifier.OnActorReloads += ReloadWeapon;
        }
        private void ReloadWeapon() => _currentWeapon.Reload();
        private void AttackByWeapon(Vector2 direction) => _currentWeapon.Shoot(direction);
        public void Dispose()
        {
            _notifier.OnActorAttacks += AttackByWeapon;
            _notifier.OnActorReloads += ReloadWeapon;
        }
    }
}