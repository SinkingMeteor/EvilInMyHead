using Sheldier.Common;
using Sheldier.Item;
using UnityEngine;

namespace Sheldier.Actors.Hand
{
    public class ActorsHand : MonoBehaviour, IExtraActorModule, ITickListener
    {
        public int Priority => 0;
        
        [SerializeField] private HandView actorHandObject;
        
        private ActorTransformHandler _transformHandler;
        private SimpleItem _currentItem;
        private NullItem _nullItem;
        private Actor _actor;
        private TickHandler _tickHandler;

        public void Initialize(ActorInternalData data)
        {
            _actor = data.Actor;
            _transformHandler = data.ActorTransformHandler;
            _tickHandler = data.TickHandler;
            _nullItem = new NullItem();
            _currentItem = _nullItem;
            actorHandObject.Initialize(_tickHandler);
            _tickHandler.AddListener(this);
            _actor.Notifier.OnItemAddedToInventory += Equip;
        }
        
        public void Equip(SimpleItem item)
        {
            if (!item.IsEquippable)
                return;
            if (_currentItem != _nullItem)
                _currentItem.Unequip();
            _currentItem = item;
            _currentItem.Equip(actorHandObject, _actor);
            _actor.InventoryModule.SetEquipped(true);
        }
        public void UnEquip()
        {
            if (_currentItem == _nullItem)
                return;
            _currentItem.Unequip();
            _actor.InventoryModule.SetEquipped(false);
            _currentItem = _nullItem;
        }
        public void Tick()
        {
            RotateHand();
        }
        private void RotateHand()
        {
            var dir = _currentItem.GetRotateDirection();
            if (dir.magnitude < 0.1f) return;
            var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
            if (!_transformHandler.LooksToRight)
                angle -= 180;
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0f, 0f, angle), 0.5f); 
        }
        public void Dispose()
        {
            _actor.Notifier.OnItemAddedToInventory -= Equip;
            _tickHandler.RemoveListener(this);
            actorHandObject.Dispose();
        }

      
    }
}