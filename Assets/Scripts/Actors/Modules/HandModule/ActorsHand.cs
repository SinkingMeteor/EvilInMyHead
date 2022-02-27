using Sheldier.Common;
using Sheldier.Item;
using UnityEngine;

namespace Sheldier.Actors.Hand
{
    public class ActorsHand : MonoBehaviour, IExtraActorModule, ITickListener
    {
        public bool IsEquipped => _currentItem != _nullItem;
        public int Priority => 0;
        
        [SerializeField] private HandView actorHandObject;
        
        private ActorTransformHandler _transformHandler;
        private ActorNotifyModule _notifier;
        private SimpleItem _currentItem;
        private NullItem _nullItem;
        private Actor _actor;
        private TickHandler _tickHandler;

        public void Initialize(ActorInternalData data)
        {
            _actor = data.Actor;
            _transformHandler = data.ActorTransformHandler;
            _tickHandler = data.TickHandler;
            _notifier = data.Notifier;
            _nullItem = new NullItem();
            _currentItem = _nullItem;
            actorHandObject.Initialize(_tickHandler);
            _tickHandler.AddListener(this);
            _notifier.OnItemAddedToInventory += Equip;
        }
        
        public void Equip(SimpleItem item)
        {
            if (!item.IsEquippable)
                return;
            if (IsEquipped)
                _currentItem.Unequip();
            _currentItem = item;
            _currentItem.Equip(actorHandObject, _actor);
        }
        public void UnEquip()
        {
            if (!IsEquipped)
                return;
            _currentItem.Unequip();
            _currentItem = _nullItem;
        }
        public void Tick()
        {
            RotateHand();
        }
        private void RotateHand()
        {
            var dir = _currentItem.GetRotateDirection();
            if(dir == Vector2.zero)
                return;
            var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
            if (!_transformHandler.LooksToRight)
                angle -= 180;
            transform.rotation = Quaternion.Euler(0f, 0f, angle); 
        }
        public void Dispose()
        {
            _notifier.OnItemAddedToInventory -= Equip;
            _tickHandler.RemoveListener(this);
            actorHandObject.Dispose();
        }

      
    }
}