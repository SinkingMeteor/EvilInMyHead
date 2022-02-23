using Sheldier.Item;
using UnityEngine;

namespace Sheldier.Actors.Hand
{
    public class ActorsHand : MonoBehaviour, IExtraActorModule
    {
        public bool IsEquipped => _currentItem != null;
        public int Priority => 0;
        
        [SerializeField] private HandView actorHandObject;
        
        private ActorTransformHandler _transformHandler;
        private ActorNotifyModule _notifier;
        private SimpleItem _currentItem;

        public void Initialize(ActorInternalData data)
        {
            _transformHandler = data.ActorTransformHandler;
            _notifier = data.Notifier;
            _notifier.OnItemAddedToInventory += Equip;
        }
        
        public void Equip(SimpleItem item)
        {
            if (!item.IsEquippable)
                return;
            if (IsEquipped)
                _currentItem.Unequip();
            actorHandObject.AddItem(item.ItemConfig.Icon);
            _currentItem = item;
            _currentItem.Equip(actorHandObject);
        }
        public void UnEquip()
        {
            if (!IsEquipped)
                return;
            actorHandObject.ClearItem();
            _currentItem.Unequip();
        }
        
        public void RotateHand(Vector2 dir)
        {
            var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
           // angle = _currentItemHandler.GetHandRotation(angle);
            if (!_transformHandler.LooksToRight)
                angle -= 180;
            actorHandObject.transform.rotation = Quaternion.Slerp(actorHandObject.transform.rotation, Quaternion.Euler(0f, 0f, angle), 0.75f); 
        }
        public void Dispose()
        {
        }
    }
}