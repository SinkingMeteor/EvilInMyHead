using System;
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
        
        [SerializeField] private HandView actorHandObject;
        
        private ActorTransformHandler _transformHandler;
        private IItem _currentEquippable;
        private ActorNotifyModule _notifier;
        private IHandInputReceiver _currentInputReceiver;
        public void Initialize(ActorInternalData data)
        {
            _transformHandler = data.ActorTransformHandler;
            _notifier = data.Notifier;
            _notifier.OnWeaponPickedUp += EquipWeapon;
            _currentInputReceiver = new BaseHandInputReceiver();
        }

        public void RotateHand(Vector2 dir)
        {
            var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
            angle = _currentInputReceiver.GetHandRotation(angle);
            if (!_transformHandler.LooksToRight)
                angle -= 180;
            actorHandObject.transform.rotation = Quaternion.Slerp(actorHandObject.transform.rotation, Quaternion.Euler(0f, 0f, angle), 0.75f); 
        }
        private void SetItemSprite(IItem activeItem)
        {
            var icon = activeItem.ItemConfig.Icon;
            _currentEquippable = activeItem;
            actorHandObject.AddItem(icon);
        }

        private void EquipWeapon(GunWeapon weapon)
        {
            if (_currentEquippable != null)
                return;
            SetItemSprite(weapon);
            _currentInputReceiver = new WeaponedHandInputReceiver(weapon, _notifier, actorHandObject);
        }
        
        public void UnEquip()
        {
            if (_currentEquippable == null)
                return;
            if (_currentInputReceiver == null)
                return;
            actorHandObject.ClearItem();
            _currentInputReceiver.Dispose();
            _currentInputReceiver = new BaseHandInputReceiver();
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
        float GetHandRotation(float angle);
        void Dispose();
        
    }
    public class WeaponedHandInputReceiver : IHandInputReceiver
    {
        private GunWeapon _currentWeapon;
        private readonly ActorNotifyModule _notifier;
        private readonly HandView _itemView;

        public WeaponedHandInputReceiver(GunWeapon weapon, ActorNotifyModule notifier, HandView itemView)
        {
            _currentWeapon = weapon;
            _notifier = notifier;
            _itemView = itemView;
            weapon.SetWeaponView(itemView);
            notifier.OnActorAttacks += AttackByWeapon;
            notifier.OnActorReloads += ReloadWeapon;
        }
        private void ReloadWeapon() => _currentWeapon.Reload();
        private void AttackByWeapon() => _currentWeapon.Shoot();
        public float GetHandRotation(float angle)
        {
            return _currentWeapon.GetHandRotation(angle);
        }

        public void Dispose()
        {
            _notifier.OnActorAttacks += AttackByWeapon;
            _notifier.OnActorReloads += ReloadWeapon;
        }
    }
    public class BaseHandInputReceiver : IHandInputReceiver
    {
        public float GetHandRotation(float angle)
        {
            return angle;
        }

        public void Dispose()
        {
        }
    }
}