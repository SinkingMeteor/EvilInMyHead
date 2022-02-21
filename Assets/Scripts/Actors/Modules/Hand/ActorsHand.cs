using System;
using Sheldier.Item;
using Sheldier.Setup;
using UnityEngine;

namespace Sheldier.Actors.Hand
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
            _notifier = data.Notifier;
            _notifier.OnWeaponPickedUp += EquipWeapon;
            _transformHandler = data.ActorTransformHandler;
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
            _currentEquippable = activeItem;
            actorHandObject.AddItem(activeItem.ItemConfig.Icon);
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

        private void OnDrawGizmos()
        {
            
        }
    }
}