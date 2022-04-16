using System;
using Sheldier.Common;
using Sheldier.Factories;
using Sheldier.Item;
using UnityEngine;

namespace Sheldier.Actors.Hand
{
    public class ActorsHand : MonoBehaviour, IExtraActorModule, ITickListener
    {
        [SerializeField] private HandView actorHandObject;
        
        private ActorTransformHandler _transformHandler;
        private TickHandler _tickHandler;
        private ItemFactory _itemFactory;
        private SimpleItem _currentItem;
        private NullItem _nullItem;
        private Actor _actor;

        public void Initialize(ActorInternalData data)
        {
            _actor = data.Actor;
            _transformHandler = data.ActorTransformHandler;
            _tickHandler = data.TickHandler;
            _nullItem = new NullItem();
            _currentItem = _nullItem;
            actorHandObject.SetDependencies(_tickHandler, _actor.StateDataModule);
            actorHandObject.Initialize();
            _tickHandler.AddListener(this);
            _actor.InventoryModule.OnEquipItem += Equip;
            _actor.InputController.OnDropButtonPressed += UnEquip;
            _actor.OnWillRemoveControl += UnEquip;
        }

        public void SetDependencies(ItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
        }
        public void Tick()
        {
            RotateHand();
            actorHandObject.Tick();
        }
        private void Equip(ItemDynamicConfigData dynamicConfigData)
        {
            SimpleItem item = _itemFactory.GetItem(dynamicConfigData.ID);
            if (!dynamicConfigData.IsEquippable)
                return;
            if (_currentItem == item)
            {
                UnEquip();
                return;
            }
            if (_currentItem != _nullItem)
                _currentItem.Unequip();
            _currentItem = item;
            _currentItem.Equip(actorHandObject, _actor);
            _actor.InventoryModule.SetEquipped(true);
        }

        private void UnEquip()
        {
            if (_currentItem == _nullItem)
                return;
            _currentItem.Unequip();
            _actor.InventoryModule.SetEquipped(false);
            _currentItem = _nullItem;
        }

        private void RotateHand()
        {
            var dir = _currentItem.GetRotateDirection();
            var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
            if (!_transformHandler.LooksToRight)
                angle -= 180;
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0f, 0f, angle), Time.deltaTime*7.0f); 
        }
        public void Dispose()
        {
            _actor.OnWillRemoveControl -= UnEquip;
            _actor.InputController.OnDropButtonPressed -= UnEquip;
            _actor.InventoryModule.OnEquipItem -= Equip;
            _tickHandler.RemoveListener(this);
            actorHandObject.Dispose();
        }

      
    }
}